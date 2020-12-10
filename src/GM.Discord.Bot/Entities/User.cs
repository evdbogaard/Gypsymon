using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Discord.Bot.Entities
{
    public class User
    {
        public int Id { get; set; }
        public ulong DiscordId { get; set; }
        public int Balance { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public virtual List<Gypsymon> Gypsymons { get; set; }
    }
}
