using GM.Discord.Bot.Entities;
using GM.Discord.Bot.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GM.Discord.Bot.Integration
{
    public class JsonRepository<T> where T : IBaseJson
    {
        private string _fileName = $"{typeof(T).Name}.json";

        public async Task Create(T model)
        {
            var models = await Read();
            if (models.Where(m => m.Id == model.Id).ToList().Count == 0)
            {
                models.Add(model);
                await Write(models);
            }
        }

        public async Task Delete(int id) =>
            await Write((await Read()).Where(m => m.Id != id).ToList());

        public async Task<List<T>> GetAll() =>
            await Read();

        public async Task<T> GetById(int id) =>
            (await Read()).FirstOrDefault(m => m.Id == id);

        public async Task Update(int id, T model)
        {
            var models = await Read();

            var index = models.FindIndex(m => m.Id == id);
            models[index] = model;
            
            await Write(models);
        }

        private async Task<List<T>> Read()
        {
            if (System.IO.File.Exists(_fileName))
            {
                var contents = await System.IO.File.ReadAllTextAsync(_fileName);
                return JsonSerializer.Deserialize<List<T>>(contents);
            }
            return new List<T>();
        }

        private async Task Write(List<T> models)
        {
            var jsonString = JsonSerializer.Serialize(models);
            await System.IO.File.WriteAllTextAsync(_fileName, jsonString);
        }
    }
}
