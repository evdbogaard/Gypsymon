using GM.Discord.Bot.DbStuff;
using GM.Discord.Bot.Interfaces;
using GM.Discord.Bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GM.Discord.Bot.Integration
{
    public class DbRepository : IRepository
    {
        private readonly GypsyContext _gypsyContext;

        public DbRepository(GypsyContext gypsyContext)
        {
            _gypsyContext = gypsyContext;
        }

        public void Create(ServerSettingsModel model)
        {
            _gypsyContext.ServerSettings.Add(model);
            _gypsyContext.SaveChanges();
        }

        public void Delete(ulong id)
        {
            var model = _gypsyContext.ServerSettings.First(m => m.ServerId == id);
            _gypsyContext.ServerSettings.Remove(model);
            _gypsyContext.SaveChanges();
        }

        public List<ServerSettingsModel> GetAll()
        {
            return _gypsyContext.ServerSettings
                 .ToList();
        }

        public ServerSettingsModel GetById(ulong id)
        {
            return _gypsyContext.ServerSettings
                .FirstOrDefault(m => m.ServerId == id);
        }

        public void Update(ulong id, ServerSettingsModel model)
        {
            var entity = _gypsyContext.ServerSettings
                .First(m => m.ServerId == id);
            entity = model;
            _gypsyContext.Update(entity);
            _gypsyContext.SaveChanges();
        }
    }
}
