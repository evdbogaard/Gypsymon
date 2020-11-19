using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GM.Discord.Bot.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace GM.Discord.Bot
{
    public class Bot
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;
        private readonly IRepository _repository;
        private readonly string _discordToken;
        private readonly int _spawnRate;
        private readonly string _prefix;

        private Dictionary<ulong, int> spawnTracker = new Dictionary<ulong, int>();

        public Bot(DiscordSocketClient client, CommandService commands, IConfigurationRoot configuration, IServiceProvider services, IRepository repository)
        {
            // It is recommended to Dispose of a client when you are finished
            // using it, at the end of your app's lifetime.
            _client = client;
            _commands = commands;
            _services = services;
            _repository = repository;

            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += HandleCommandAsync;

            _discordToken = configuration["discordToken"];
            _spawnRate = configuration.GetValue<int>("SpawnRate");
            _prefix = configuration.GetValue<string>("Prefix");
        }

        public async Task MainAsync()
        {
            // Tokens should be considered secret data, and never hard-coded.
            await _client.LoginAsync(TokenType.Bot, _discordToken);
            await _client.StartAsync();

            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                services: _services);

            // Block the program until it is closed.
            await Task.Delay(Timeout.Infinite);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        // The Ready event indicates that the client has opened a
        // connection and it is now safe to access the cache.
        private Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser} is connected!");

            _repository.GetAll().Result.ForEach(settings =>
            {
                var chnl = _client.GetChannel(settings.SpawnChannelId) as ITextChannel;
                //chnl.SendMessageAsync("Hello world");

                //var fileName = "tux-turtle.jpg";
                //var embed = new EmbedBuilder()
                //{
                //    Title = "Hellow world",
                //    Color = Color.Green,
                //    Description = "I'm an embedded message",
                //    ImageUrl = $"attachment://{fileName}"
                //}.Build();

                ////chnl.SendMessageAsync(embed: embed);
                //chnl.SendFileAsync(fileName, embed: embed);
            });

            return Task.CompletedTask;
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasStringPrefix(_prefix, ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot || message.Author.IsWebhook)
            {
                if (spawnTracker.ContainsKey(message.Channel.Id))
                {
                    if (++spawnTracker[message.Channel.Id] >= _spawnRate)
                    {
                        var fileName = "tux-turtle.jpg";
                        var embed = new EmbedBuilder()
                        {
                            Title = "A random gypsy entered the room.",
                            Color = Color.Green,
                            Description = "There's nothing you can do about",
                            ImageUrl = $"attachment://{fileName}"
                        }.Build();

                        await message.Channel.SendFileAsync(fileName, embed: embed);

                        spawnTracker[message.Channel.Id] = 0;
                    }
                }
                else
                    spawnTracker.Add(message.Channel.Id, 1);

                return;
            }

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, message);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.

            // Keep in mind that result does not indicate a return value
            // rather an object stating if the command executed successfully.
            var result = await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _services);

            // Optionally, we may inform the user if the command fails
            // to be executed; however, this may not always be desired,
            // as it may clog up the request queue should a user spam a
            // command.
            // if (!result.IsSuccess)
            // await context.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}
