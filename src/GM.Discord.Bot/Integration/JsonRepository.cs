using GM.Discord.Bot.Interfaces;
using GM.Discord.Bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GM.Discord.Bot.Integration
{
    public class JsonRepository : IRepository
    {
        private string _fileName = "asdf.json";

        public async Task Create(ServerSettingsModel model)
        {
            var models = await Read();
            if (models.Where(m => m.ServerId == model.ServerId).ToList().Count == 0)
            {
                models.Add(model);
                await Write(models);
            }
        }

        public async Task Delete(ulong id) =>
            await Write((await Read()).Where(m => m.ServerId != id).ToList());

        public async Task<List<ServerSettingsModel>> GetAll() =>
            await Read();

        public async Task<ServerSettingsModel> GetById(ulong id) =>
            (await Read()).FirstOrDefault(m => m.ServerId == id);

        public async Task Update(ulong id, ServerSettingsModel model)
        {
            var models = await Read();

            var index = models.FindIndex(m => m.ServerId == id);
            models[index] = model;
            
            await Write(models);
        }

        private async Task<List<ServerSettingsModel>> Read()
        {
            if (System.IO.File.Exists(_fileName))
            {
                var contents = await System.IO.File.ReadAllTextAsync(_fileName);
                return JsonSerializer.Deserialize<List<ServerSettingsModel>>(contents);
            }
            return new List<ServerSettingsModel>();
        }

        private async Task Write(List<ServerSettingsModel> models)
        {
            var jsonString = JsonSerializer.Serialize(models);
            await System.IO.File.WriteAllTextAsync(_fileName, jsonString);
        }
    }
}
