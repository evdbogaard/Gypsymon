using GM.Discord.Bot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GM.Discord.Bot.Db
{
    public class GypsyContextFactory : IDesignTimeDbContextFactory<GypsyContext>
    {
        public GypsyContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.dev.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<SecretsModel>()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<GypsyContext>();
            optionsBuilder.UseNpgsql(builder.GetConnectionString("GypsyDb"));

            return new GypsyContext(optionsBuilder.Options);
        }
    }
}
