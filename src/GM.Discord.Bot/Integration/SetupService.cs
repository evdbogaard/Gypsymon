using Discord;
using Discord.WebSocket;
using GM.Discord.Bot.Interfaces2;
using GM.Discord.Bot.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void SetSpawnChannel(ulong serverId, ulong channelId)
        {
            var serverSettings = GetOrCreateServerSettings(serverId);
            serverSettings.SpawnChannelId = channelId;
            _repository.Update(serverId, serverSettings);
        }

        public ITextChannel GetSpawnChannel(ulong serverId)
        {
            var serverSettings = GetOrCreateServerSettings(serverId);
            return _client.GetChannel(serverSettings.SpawnChannelId) as ITextChannel;
        }

        private ServerSettingsModel GetOrCreateServerSettings(ulong serverId)
        {
            var settings = _repository.GetById(serverId);
            if (settings == null)
            {
                settings = new ServerSettingsModel()
                {
                    ServerId = serverId
                };
                _repository.Create(settings);
            }
            return settings;
        }
    }
}
