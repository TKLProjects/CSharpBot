using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace C_Sharp_Bot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("ping")]
        [Summary("Plays ping pong with you.")]
        public async Task Ping()
        {
            await ReplyAsync("Pong");
        }

        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
            => ReplyAsync(echo);

        [Command("math")]
        public async Task Calculate([Remainder] string equation)
        {
            string result = new DataTable().Compute(equation, null).ToString();
            //Basically to calculate from the string to find the result
            if (result == "NaN")
            {
                await Context.Channel.SendMessageAsync("Infinity or undefined");
            }
            else
            {
                await Context.Channel.SendMessageAsync(result);
            }
        };
    };

    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("userinfo")]
        [Summary("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync(
        [Summary("The (optional) user to get info from")]
        SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }
    };
}
