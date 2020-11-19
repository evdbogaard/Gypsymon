using GM.Discord.Bot.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GM.Discord.Bot.Interfaces
{
    public interface IRepository
    {
        Task Create(ServerSettings model);
        Task Update(ulong id, ServerSettings model);
        Task Delete(ulong id);

        Task<ServerSettings> GetById(ulong id);
        Task<List<ServerSettings>> GetAll();
    }
}
