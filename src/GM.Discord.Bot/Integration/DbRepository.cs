using GM.Discord.Bot.Db;
using GM.Discord.Bot.Entities;
using GM.Discord.Bot.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GM.Discord.Bot.Integration
{
    public class DbRepository : IRepository
    {
        private readonly GypsyContext _gypsyContext;

        public DbRepository(GypsyContext gypsyContext)
        {
            _gypsyContext = gypsyContext;
        }

        public async Task Create(ServerSettings model)
        {
            await _gypsyContext.ServerSettings.AddAsync(model);
            await _gypsyContext.SaveChangesAsync();
        }

        public async Task Delete(ulong id)
        {
            var model = await _gypsyContext.ServerSettings.FirstAsync(m => m.ServerId == id);
            _gypsyContext.ServerSettings.Remove(model);
            await _gypsyContext.SaveChangesAsync();
        }

        public async Task<List<ServerSettings>> GetAll() =>
            await _gypsyContext.ServerSettings
                 .ToListAsync();

        public async Task<ServerSettings> GetById(ulong id) =>
            await _gypsyContext.ServerSettings
                .FirstOrDefaultAsync(m => m.ServerId == id);

        public async Task Update(ulong id, ServerSettings model)
        {
            var entity = await _gypsyContext.ServerSettings
                .FirstAsync(m => m.ServerId == id);
            entity = model;
            _gypsyContext.Update(entity);
            await _gypsyContext.SaveChangesAsync();
        }
    }
}
