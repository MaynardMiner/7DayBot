using Commands.Actions;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading;
using System.Threading.Tasks;


namespace Commands.Server
{
    public class Server_Commands : BaseCommandModule
    {
        [Command("restart")] // let's define this method as a command
        [Description("Restarts the 7dtd server")] // this will be displayed to tell users what this command does when they invoke help
        public async Task Restart_Server(CommandContext ctx) // this command takes no arguments
        {
            CommandContext tempctx = ctx;
            Thread thread = new Thread(() => Restart.ToDo.Start(tempctx).GetAwaiter().GetResult());
            thread.Start();
            await Task.Delay(500);
        }

        [Command("check")] // let's define this method as a command
        [Description("Checks if threading works")] // this will be displayed to tell users what this command does when they invoke help
        public async Task Check_Threading(CommandContext ctx) // this command takes no arguments
        {
            CommandContext tempctx = ctx;
            Thread thread = new Thread(() => Check.ToDo.Start(tempctx).GetAwaiter().GetResult());
            thread.Start();
            await Task.Delay(30000);
        }

    }
}
