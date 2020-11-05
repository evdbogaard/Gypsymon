using GM.Discord.Bot.Interfaces;
using GM.Discord.Bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GM.Discord.Bot.Integration
{
    public class MemoryRepository : IRepository
    {
        private Dictionary<ulong, ServerSettingsModel> _storage = new Dictionary<ulong, ServerSettingsModel>();

        public void Create(ServerSettingsModel model)
        {
            if (!_storage.ContainsKey(model.ServerId))
                _storage.Add(model.ServerId, model);
        }

        public void Delete(ulong id) =>
            _storage.Remove(id);

        public List<ServerSettingsModel> GetAll() =>
            _storage.Values.ToList();

        public ServerSettingsModel GetById(ulong id)
        {
            _storage.TryGetValue(id, out var model);
            return model;
        }

        public void Update(ulong id, ServerSettingsModel model)
        {
            if (_storage.ContainsKey(model.ServerId))
                _storage[id] = model;
        }
    }
}
