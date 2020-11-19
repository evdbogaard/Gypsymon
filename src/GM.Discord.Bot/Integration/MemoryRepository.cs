using GM.Discord.Bot.Entities;
using GM.Discord.Bot.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GM.Discord.Bot.Integration
{
    public class MemoryRepository : IRepository
    {
        private Dictionary<ulong, ServerSettings> _storage = new Dictionary<ulong, ServerSettings>();

        public async Task Create(ServerSettings model)
        {
            if (!_storage.ContainsKey(model.ServerId))
                _storage.Add(model.ServerId, model);
        }

        public async Task Delete(ulong id) =>
            _storage.Remove(id);

        public async Task<List<ServerSettings>> GetAll() =>
            _storage.Values.ToList();

        public async Task<ServerSettings> GetById(ulong id)
        {
            _storage.TryGetValue(id, out var model);
            return model;
        }

        public async Task Update(ulong id, ServerSettings model)
        {
            if (_storage.ContainsKey(model.ServerId))
                _storage[id] = model;
        }
    }
}
