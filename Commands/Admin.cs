using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _7DayBot;
using DSharpPlus.Entities;
using Commands.Globals;

namespace Commands.Admin
{
    public static class Connectivity
    {
        public static void Daily_Reset( )
        {
            bool morning_reset = false;
            bool afternoon_reset = false;

            while (true)
            {
                var date = DateTime.Now;
                ///Morning reset- ONly once.
                if (date.Hour == 4 && date.Minute == 0 && morning_reset == false)
                {
                    Actions.ToDo.Restart().GetAwaiter().GetResult();
                    morning_reset = true;
                }
                // Afternoon reset- Only once.
                if (date.Hour == 16 && date.Minute == 0 && afternoon_reset == false)
                {
                    Actions.ToDo.Restart().GetAwaiter().GetResult();
                    afternoon_reset = true;
                }
                /// Reset flags at 11pm.
                if (date.Hour == 23 && date.Minute == 0)
                {
                    morning_reset = false;
                    afternoon_reset = false;
                }
                /// Wait 10 seconds, check again.
                Thread.Sleep(10000);
            }
        }

        public static void Check_Server()
        {
            int port = Config.Port; //<--- This is your value
            string ip = Config.Ip;
            bool StatusUp = false;
            bool first = true;

            while (true)
            {
                if (Global.cl != null)
                {
                    TcpClient tc = new TcpClient();
                    try
                    {
                        tc.Connect(ip, port);
                    }
                    catch
                    {

                    }
                        bool stat = tc.Connected;
                        if (stat)
                        {
                            if (!StatusUp || first)
                            {
                                DiscordActivity activity = new DiscordActivity("Server is Online", ActivityType.Streaming);
                                Global.cl.UpdateStatusAsync(activity).GetAwaiter().GetResult();
                                Console.WriteLine("Changing Status on Discord...");
                                StatusUp = true;
                            }
                        }
                        else
                        {
                            if (StatusUp || first)
                            {
                                DiscordActivity activity = new DiscordActivity("Server is Offline", ActivityType.Streaming);
                                Global.cl.UpdateStatusAsync(activity).GetAwaiter().GetResult();
                                Console.WriteLine("Changing Status on Discord...");
                                StatusUp = false;
                            }
                        }
                    tc.Close();
                    tc.Dispose();
                    first = false;
                    Thread.Sleep(20000);
                }
                Thread.Sleep(10000);
            }
        }
    }
}
