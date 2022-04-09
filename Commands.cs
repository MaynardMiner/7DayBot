using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Diagnostics;
using System.Threading;


namespace _7DayBot
{
        public class ExampleUngrouppedCommands : BaseCommandModule
        {
            [Command("!restart")] // let's define this method as a command
            [Description("Restarts the 7dtd server")] // this will be displayed to tell users what this command does when they invoke help
            public async Task Restart(CommandContext ctx) // this command takes no arguments
            {
            bool allclosed = true;
            // First find the process
            Process[] processes = Process.GetProcessesByName("7DaysToDieServer");
                await ctx.RespondAsync("There were " + processes.Length + " processes running...Attempting to close them all....Please Wait.");
                foreach(Process p in processes)
                {
                    if(!p.HasExited)
                    {
                        p.CloseMainWindow();
                    }
                }
                await Task.Delay(10000);
                foreach(Process p in processes)
                {
                    if(!p.HasExited)
                    {
                        allclosed = false;
                        await ctx.RespondAsync("Process id " + p.Id + " did not close gracefully. Will try to force");
                        p.Kill();
                    }
                }
                if(!allclosed)
                {
                    await Task.Delay(5000);
                    allclosed = true;
                    foreach (Process p in processes)
                    {
                        if (!p.HasExited)
                        {
                            allclosed = false;
                        }
                    }
                }
                if(!allclosed)
                {
                    await ctx.RespondAsync("Could not restart server, notify admin...");
                    return;
                }
                await ctx.RespondAsync("Restarting Server...");
                System.Diagnostics.Process.Start(@"C:\Program Files(x86)\Steam\steamapps\common\7 Days to Die Dedicated Server\startdedicated.bat");
                await Task.Delay(5000);
                processes = Process.GetProcessesByName("7DaysToDieServer");
                if(processes.Length > 0)
                {
                    await ctx.RespondAsync("Server has restarted...");
                }
                return;
            }

        }
 }
