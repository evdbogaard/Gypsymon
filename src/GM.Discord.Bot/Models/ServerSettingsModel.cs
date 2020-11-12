using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Discord.Bot.Models
{
    public class ServerSettingsModel
    {
        public int Id { get; set; }
        public ulong ServerId { get; set; }
        public ulong SpawnChannelId { get; set; }

    }
}
