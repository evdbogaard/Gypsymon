using GM.Discord.Bot.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GM.Discord.Bot.Entities
{
    public class Spawn : IDiscordServer
    {
        [Key]
        public ulong ServerId { get; set; }
        public string Name { get; set; }
        public string[] AlternativeNames { get; set; }
        public bool Caught { get; set; }
    }
}
