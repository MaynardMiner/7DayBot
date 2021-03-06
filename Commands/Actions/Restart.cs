using DSharpPlus.CommandsNext;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Commands.Globals;

namespace Commands.Restart
{
    public class ToDo
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public static async Task Start(CommandContext ctx = null)
        {
            ///Parameters
            string filepath = Config.AppPath;
            string appname = Config.AppName;
            bool allclosed = true;
            Process[] processes = Process.GetProcessesByName(appname);
            bool Isctx = ctx != null;


            /// Notify commamd was recieved
            if (Isctx)
            {
                await ctx.RespondAsync("There were " + processes.Length + " processes running...Attempting to close them all....Please Wait.");
            }
            Console.WriteLine("There were " + processes.Length + " processes running...Attempting to close them all....Please Wait.");

            /// Close each process gently
            foreach (Process p in processes)
            {
                if (!p.HasExited)
                {
                    IntPtr h = p.MainWindowHandle;
                    SetForegroundWindow(h);
                    p.CloseMainWindow();
                    await Task.Delay(3000);
                    if (Config.NeedsEnter)
                    {
                        SendKeys.SendWait("{ENTER}");
                    }
                }
            }

            /// Give some time for them to close
            await Task.Delay(30000);

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
                    Console.WriteLine("Process id " + p.Id + " did not close gracefully. Will try to force");
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
                Console.WriteLine("Could not restart server, notify admin...");
                return;
            }

            /// Restart the Server now.
            if (Isctx)
            {
                await ctx.RespondAsync("Restarting Server...");
            }
            Console.WriteLine("Restarting Server...");

            Process proc = new Process();
            proc.StartInfo.FileName = filepath;
            proc.StartInfo.CreateNoWindow = false;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
            await Task.Delay(5000);

            /// Confirm server was restarted.
            processes = Process.GetProcessesByName(appname);
            if (processes.Length > 0)
            {
                if (Isctx)
                {
                    await ctx.RespondAsync("Server has restarted...");
                }
                Console.WriteLine("Server has restarted...");
            }
            return;
        }
    }
}
