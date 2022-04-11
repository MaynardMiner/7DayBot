using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Diagnostics;
using System.Windows.Forms;


namespace Commands.Server
{
    public class Server_Commands : BaseCommandModule
        {
            [Command("restart")] // let's define this method as a command
            [Description("Restarts the 7dtd server")] // this will be displayed to tell users what this command does when they invoke help
            public async Task Restart_Server(CommandContext ctx) // this command takes no arguments
            {
                await Actions
            }
        }
 }
