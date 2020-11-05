using GM.Discord.Bot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Discord.Bot.Interfaces
{
    public interface IRepository
    {
        void Create(ServerSettingsModel model);
        void Update(ulong id, ServerSettingsModel model);
        void Delete(ulong id);

        ServerSettingsModel GetById(ulong id);
        List<ServerSettingsModel> GetAll();
    }
}
