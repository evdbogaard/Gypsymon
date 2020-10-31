using Discord.Commands;
using GM.Discord.Bot.Integration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GM.Discord.Bot.Modules
{
    [RequireContext(ContextType.Guild)]
    public class SetupModule : ModuleBase<SocketCommandContext>
    {
        private readonly SetupService _setupService;

        public SetupModule(SetupService setupService)
        {
            _setupService = setupService;
        }

        [Command("spawn")]
        public async Task Spawn()
        {
            _setupService.SpawnChannelId = Context.Channel.Id;
            await ReplyAsync($"Spawning set to channel {Context.Channel.Name}.");
        }
    }
}
