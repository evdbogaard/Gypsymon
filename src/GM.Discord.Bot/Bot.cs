using Discord;
using Discord.Commands;
using Discord.WebSocket;
using GM.Discord.Bot.Interfaces2;
using Microsoft.Extensions.Configuration;
using System;
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

            _repository.GetAll().ForEach(settings =>
            {
                var chnl = _client.GetChannel(settings.SpawnChannelId) as ITextChannel;
                chnl.SendMessageAsync("Hello world");
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
            if (!(message.HasCharPrefix('!', ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot || message.Author.IsWebhook)
                return;

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
