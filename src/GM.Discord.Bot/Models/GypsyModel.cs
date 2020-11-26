using GM.Discord.Bot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Discord.Bot.Models
{
    public class GypsyModel : IBaseJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] AlternativeNames { get; set; }
        public string Image { get; set; }
    }
}
