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
            while(true)
            {
                var date = DateTime.Now;
                if(date.Hour == 4 && date.Minute == 0 || date.Hour == 16 && date.Minute == 0)
                {
                    Actions.ToDo.Restart().GetAwaiter().GetResult();
                }
                Thread.Sleep(10000);
            }
        }
    }
}
