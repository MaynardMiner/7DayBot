using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Check
{
    class ToDo
    {
        public static async Task Start(CommandContext ctx = null) 
        {
            bool Isctx = ctx != null;

            if(Isctx)
            {
                await ctx.RespondAsync("This is a test.");
            }

            await Task.Delay(10000);
        }
    }
}
