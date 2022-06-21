using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static dolPhin.clean.print;

namespace dolPhin
{
    public class Initialize
    {
        public static void Run()
        {
            Console.Title = "dolPhin";
            Console.ForegroundColor = ConsoleColor.White;

            

            string LHOST = localIP();
            string RHOST = "*";
            string LPORT = "9000";

            shell.dshell.variables.Add("lhost", LHOST);
            shell.dshell.variables.Add("rhost", RHOST);
            shell.dshell.variables.Add("lport", LPORT);
            shell.dshell.variables.Add("cprompt", "0");

            printf(@"               ^_^ ", ConsoleColor.Blue, "                                             "+"\n", ConsoleColor.White);
            printf(@",,,__________.\   /", ConsoleColor.Blue, " ---> Welcome to dolPhin ", ConsoleColor.White, "v1.0.0.2            "+"\n", ConsoleColor.Yellow);
            printf(@"\   ", ConsoleColor.Blue, "*", ConsoleColor.DarkGray, "            / ", ConsoleColor.Blue, "----> In this Version we have over ", ConsoleColor.White, "?", ConsoleColor.Blue, " features"+"\n", ConsoleColor.White);
            printf(@" \_____________./  ", ConsoleColor.Blue, "----> Have fun creating your own Shells!     "+"\n\n", ConsoleColor.White);

            printf(@"           =[ ", ConsoleColor.White, "dolphin v1.0.2-dev          ", ConsoleColor.Yellow, "]\n", ConsoleColor.White);
            printf(@"+ ------ --=[ ", ConsoleColor.White, "2 customizable options      ", ConsoleColor.Gray  , "]\n", ConsoleColor.White);
            printf(@"+ ------ --=[ ", ConsoleColor.White, "** This is dolPhin 1-dev ** ", ConsoleColor.Gray  , "]\n\n", ConsoleColor.White);
        }       

        public static string localIP()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address.ToString();
            }
        }
    }
}
