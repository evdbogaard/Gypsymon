using GM.Discord.Bot.Db;
using GM.Discord.Bot.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GM.Discord.Bot.Extensions
{
    static public class GypsyContextExtensions
    {
        static public async Task AddOrUpdateEntity<T>(this GypsyContext dbContext, T newEntity) where T : class, IDiscordServer
        {
            var entity = await dbContext.Set<T>().FindAsync(newEntity.ServerId);
            if (entity == null)
                await dbContext.Set<T>().AddAsync(newEntity);
            else
            {
                dbContext.Entry(entity).State = EntityState.Detached;
                dbContext.Set<T>().Update(newEntity);
            }
        }
    }
}
