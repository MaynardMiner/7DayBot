using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Reset
{
    public static class Server_Reset
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
    }
}
