using GM.Discord.Bot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Discord.Bot.Extensions
{
    static public class GypsyModelExtensions
    {
        static public GypsyModel GetRandom(this List<GypsyModel> gypsyModels)
        {
            var random = new Random();
            return gypsyModels[random.Next(gypsyModels.Count)];
        }
    }
}
