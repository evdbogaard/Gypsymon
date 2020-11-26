using GM.Discord.Bot.Entities;
using Microsoft.EntityFrameworkCore;

namespace GM.Discord.Bot.Db
{
    public class GypsyContext : DbContext
    {
        public DbSet<ServerSettings> ServerSettings { get; set; }
        public DbSet<Spawn> Spawns { get; set; }

        public GypsyContext(DbContextOptions<GypsyContext> options) : base(options)
        {

        }
    }
}
