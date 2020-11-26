using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Discord.Bot.Interfaces
{
    public interface IBaseJson
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
