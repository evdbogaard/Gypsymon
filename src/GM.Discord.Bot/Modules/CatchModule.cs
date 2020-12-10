using Discord.Commands;
using GM.Discord.Bot.Integration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GM.Discord.Bot.Modules
{
	public class CatchModule : ModuleBase<SocketCommandContext>
	{
        private readonly GypsyService _gypsyService;

        public CatchModule(GypsyService gypsyService)
        {
            this._gypsyService = gypsyService;
        }

		[RequireContext(ContextType.Guild)]
		[Command("catch")]
		public async Task Catch(string name)
		{
			if (await _gypsyService.TryCatch(Context.Guild.Id, Context.Channel.Id, name, Context.User.Id))
				await ReplyAsync("Hurray, you caught it!!!");
			else
				await ReplyAsync("What are you doing?");
		}

		[RequireContext(ContextType.Guild)]
		[Command("join")]
		public async Task Join()
		{
			if (await _gypsyService.Join(Context.User.Id))
				await ReplyAsync($"{Context.User.Mention} has joined!");
			else
				await ReplyAsync("No...just no!");
		}

		[Command("list")]
		public async Task List()
		{
			await ReplyAsync(await _gypsyService.List(Context.User.Id));
		}
	}
}
