using Discord;
using Discord.WebSocket;
using GM.Discord.Bot.Db;
using GM.Discord.Bot.Entities;
using GM.Discord.Bot.Extensions;
using GM.Discord.Bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.Discord.Bot.Integration
{
    public class GypsyService
    {
        private readonly JsonRepository<GypsyModel> _gypsyRepository;
        private readonly GypsyContext _dbContext;
        private readonly DiscordSocketClient _client;

        public GypsyService(JsonRepository<GypsyModel> gypsyRepository, GypsyContext gypsyContext, DiscordSocketClient client) =>
            (_gypsyRepository, _dbContext, _client) = (gypsyRepository, gypsyContext, client);

        public async Task CreateSpawn(ulong serverId, string prefix)
        {
            var gypsy = (await _gypsyRepository.GetAll()).GetRandom(); // _gypsyModels.GetRandom();

            var fileName = gypsy.Image; // "tux-turtle.jpg";
            var embed = new EmbedBuilder()
            {
                Title = "A random gypsy entered the room.",
                Color = Color.Green,
                Description = $"Use `{prefix}catch name` to catch it",
                ImageUrl = $"attachment://random.jpg"
            }.Build();

            var stream = System.IO.File.OpenRead(gypsy.Image);

            var spawn = new Spawn
            {
                Name = gypsy.Name,
                AlternativeNames = gypsy.AlternativeNames,
                Caught = false,
                ServerId = serverId
            };
            await _dbContext.AddOrUpdateEntity(spawn);
            await _dbContext.SaveChangesAsync();

            var serverSettings = await _dbContext.ServerSettings.FirstOrDefaultAsync(s => s.ServerId == serverId);
            if (serverSettings == null || serverSettings.SpawnChannelId == 0)
                return;

            var spawnChannel = _client.GetChannel(serverSettings.SpawnChannelId) as ITextChannel;
            await spawnChannel.SendFileAsync(stream, "random.jpg", embed: embed);
        }

        public async Task<bool> TryCatch(ulong serverId, ulong channelId, string guess)
        {
            // Check correct channel
            var serverSettings = await _dbContext.ServerSettings.FirstOrDefaultAsync(s => s.ServerId == serverId && s.SpawnChannelId == channelId);
            if (serverSettings == null)
                return false;

            // Check active spawn
            var spawn = await _dbContext.Spawns.FirstOrDefaultAsync(s => s.ServerId == serverId);
            if (spawn == null || spawn.Caught)
                return false;

            // Compare names
            var spawnNames = spawn.AlternativeNames
                .Select(s => s.ToLowerInvariant())
                .ToList();
            spawnNames.Add(spawn.Name.ToLowerInvariant());

            if (spawnNames.Contains(guess.ToLowerInvariant()))
            {
                spawn.Caught = true;
                _dbContext.Update(spawn);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
