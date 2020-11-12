using GM.Discord.Bot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GM.Discord.Bot.DbStuff
{
    public class GypsyContext : DbContext
    {
        public DbSet<ServerSettingsModel> ServerSettings { get; set; }

        public GypsyContext(DbContextOptions<GypsyContext> options) : base(options)
        {

        }
    }
}
