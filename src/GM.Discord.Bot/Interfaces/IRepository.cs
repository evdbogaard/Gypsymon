using GM.Discord.Bot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GM.Discord.Bot.Interfaces
{
    public interface IRepository
    {
        Task Create(ServerSettingsModel model);
        Task Update(ulong id, ServerSettingsModel model);
        Task Delete(ulong id);

        Task<ServerSettingsModel> GetById(ulong id);
        Task<List<ServerSettingsModel>> GetAll();
    }
}
