using Discord;
using Discord.WebSocket;
using GM.Discord.Bot.Interfaces;
using GM.Discord.Bot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GM.Discord.Bot.Integration
{
    public class SetupService
    {
        private readonly IRepository _repository;
        private readonly DiscordSocketClient _client;

        public SetupService(IRepository repository, DiscordSocketClient client)
        {
            _repository = repository;
            _client = client;
        }

        public async Task SetSpawnChannel(ulong serverId, ulong channelId)
        {
            var serverSettings = await GetOrCreateServerSettings(serverId);
            serverSettings.SpawnChannelId = channelId;
            await _repository.Update(serverId, serverSettings);
        }

        public async Task<ITextChannel> GetSpawnChannel(ulong serverId)
        {
            var serverSettings = await GetOrCreateServerSettings(serverId);
            return _client.GetChannel(serverSettings.SpawnChannelId) as ITextChannel;
        }

        private async Task<ServerSettingsModel> GetOrCreateServerSettings(ulong serverId)
        {
            var settings = await _repository.GetById(serverId);
            if (settings == null)
            {
                settings = new ServerSettingsModel()
                {
                    ServerId = serverId
                };
                await _repository.Create(settings);
            }
            return settings;
        }
    }
}
