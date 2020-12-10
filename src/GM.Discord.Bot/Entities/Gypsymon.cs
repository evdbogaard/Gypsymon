using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Discord.Bot.Entities
{
    public class Gypsymon
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
