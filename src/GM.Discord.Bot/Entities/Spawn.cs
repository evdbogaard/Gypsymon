using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Discord.Bot.Entities
{
    public class Spawn
    {
        public int Id { get; set; }
        public ulong ServerId { get; set; }
        public string Name { get; set; }
        public string[] AlternativeNames { get; set; }
        public bool Caught { get; set; }
    }
}
