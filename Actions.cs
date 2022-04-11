using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commands.Actions
{
   public class ToDo
    {
        public async Task Restart(CommandContext ctx = null)
        {
            ///Parameters
            string filepath = @"C:\Program Files(x86)\Steam\steamapps\common\7 Days to Die Dedicated Server\startdedicated.bat";
            bool allclosed = true;
            Process[] processes = Process.GetProcessesByName("7DaysToDieServer");
            bool Isctx = ctx != null;

            /// Notify commamd was recieved
            if (Isctx)
            {
                await ctx.RespondAsync("There were " + processes.Length + " processes running...Attempting to close them all....Please Wait.");
            }

            /// Close each process gently
            foreach (Process p in processes)
            {
                if (!p.HasExited)
                {
                    p.CloseMainWindow();
                    await Task.Delay(3000);
                    SendKeys.Send("{ENTER}");
                }
            }

            /// Give some time for them to close
            await Task.Delay(5000);

            /// Check if it closed- If it didn't, kill it. Inform user
            foreach (Process p in processes)
            {
                if (!p.HasExited)
                {
                    allclosed = false;
                    if (Isctx)
                    {
                        await ctx.RespondAsync("Process id " + p.Id + " did not close gracefully. Will try to force");
                    }
                    p.Kill();
                }
            }

            /// Ensure everything is closed now.
            /// If not notify user
            if (!allclosed)
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
            if (!allclosed)
            {
                if (Isctx)
                {
                    await ctx.RespondAsync("Could not restart server, notify admin...");
                }
                return;
            }

            /// Restart the Server now.
            if(Isctx)
            {
                await ctx.RespondAsync("Restarting Server...");
            }
            System.Diagnostics.Process.Start(@"C:\Program Files(x86)\Steam\steamapps\common\7 Days to Die Dedicated Server\startdedicated.bat");
            await Task.Delay(5000);

            /// Confirm server was restarted.
            processes = Process.GetProcessesByName("7DaysToDieServer");
            if (processes.Length > 0)
            {
                if (Isctx)
                {
                    await ctx.RespondAsync("Server has restarted...");
                }
            }
            return;


        }
    }
}
