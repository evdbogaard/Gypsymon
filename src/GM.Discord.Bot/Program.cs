using Discord.Commands;
using Discord.WebSocket;
using GM.Discord.Bot.Db;
using GM.Discord.Bot.Integration;
using GM.Discord.Bot.Interfaces;
using GM.Discord.Bot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace GM.Discord.Bot
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var env = args.First();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

            if (env == "dev")
                builder.AddUserSecrets<SecretsModel>();
            Configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddDbContext<GypsyContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("GypsyDb")))
                .AddSingleton(Configuration)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<Bot>()
                .AddSingleton<SetupService>()
                .AddTransient<IRepository, DbRepository>()
                .BuildServiceProvider();

            serviceProvider.GetService<Bot>().MainAsync().Wait();
        }        
    }
}