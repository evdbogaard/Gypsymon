using GM.Discord.Bot.Interfaces2;
using GM.Discord.Bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GM.Discord.Bot.Integration
{
    public class JsonRepository : IRepository
    {
        private string _fileName = "asdf.json";

        public void Create(ServerSettingsModel model)
        {
            var models = Read();
            if (models.Where(m => m.ServerId == model.ServerId).ToList().Count == 0)
            {
                models.Add(model);
                Write(models);
            }
        }

        public void Delete(ulong id) =>
            Write(Read().Where(m => m.ServerId != id).ToList());

        public List<ServerSettingsModel> GetAll() =>
            Read();

        public ServerSettingsModel GetById(ulong id) =>
            Read().FirstOrDefault(m => m.ServerId == id);

        public void Update(ulong id, ServerSettingsModel model)
        {
            var models = Read();

            var index = models.FindIndex(m => m.ServerId == id);
            models[index] = model;
            
            Write(models);
        }

        private List<ServerSettingsModel> Read()
        {
            if (System.IO.File.Exists(_fileName))
            {
                var contents = System.IO.File.ReadAllText(_fileName);
                return JsonSerializer.Deserialize<List<ServerSettingsModel>>(contents);
            }
            return new List<ServerSettingsModel>();
        }

        private void Write(List<ServerSettingsModel> models)
        {
            var jsonString = JsonSerializer.Serialize(models);
            System.IO.File.WriteAllText(_fileName, jsonString);
        }
    }
}
