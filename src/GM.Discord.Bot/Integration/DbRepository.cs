using GM.Discord.Bot.DbStuff;
using GM.Discord.Bot.Interfaces;
using GM.Discord.Bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task Create(ServerSettingsModel model)
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

        public async Task<List<ServerSettingsModel>> GetAll() =>
            await _gypsyContext.ServerSettings
                 .ToListAsync();

        public async Task<ServerSettingsModel> GetById(ulong id) =>
            await _gypsyContext.ServerSettings
                .FirstOrDefaultAsync(m => m.ServerId == id);

        public async Task Update(ulong id, ServerSettingsModel model)
        {
            var entity = await _gypsyContext.ServerSettings
                .FirstAsync(m => m.ServerId == id);
            entity = model;
            _gypsyContext.Update(entity);
            await _gypsyContext.SaveChangesAsync();
        }
    }
}
