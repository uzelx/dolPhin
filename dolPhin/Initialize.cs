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

            printf(@"                   *** ####### ***"+"\n", ConsoleColor.Blue); 
            printf(@"               *#####################*"+"\n", ConsoleColor.Blue); 
            printf(@"           *#############################*"+"\n", ConsoleColor.Blue); 
            printf(@"        *###################################*"+"\n", ConsoleColor.Blue); 
            printf(@"      *#######################################*"+"\n", ConsoleColor.Blue); 
            printf(@"    *###########################################*"+"\n", ConsoleColor.Blue); 
            printf(@"   *#################     ######              ###*"+"\n", ConsoleColor.Blue); 
            printf(@"  *################       #########      #########*"+"\n", ConsoleColor.Blue); 
            printf(@" *################         ########      ##########*"+"\n", ConsoleColor.Blue); 
            printf(@" *######                                 ##########*"+"\n", ConsoleColor.Blue); 
            printf(@" *#####                                  ##########*"+"\n", ConsoleColor.Blue); 
            printf(@" *#####                                  ##########*"+"\n", ConsoleColor.Blue); 
            printf(@" *#####                                  ##########*"+"\n", ConsoleColor.Blue); 
            printf(@"  *####                                 ##########*"+"\n", ConsoleColor.Blue); 
            printf(@"   *#############################################*"+"\n", ConsoleColor.Blue); 
            printf(@"    *###########################################*"+"\n", ConsoleColor.Blue); 
            printf(@"      *########################################*"+"\n", ConsoleColor.Blue); 
            printf(@"        *###################################*"+"\n", ConsoleColor.Blue); 
            printf(@"           *#############################*"+"\n", ConsoleColor.Blue); 
            printf(@"               *#####################*"+"\n", ConsoleColor.Blue); 
            printf(@"                  *** ####### ***" + "\n\n", ConsoleColor.Blue); 

            printf(@"           =[ ", ConsoleColor.White, "dolphin v1.0.1-dev          ", ConsoleColor.Yellow, "]\n", ConsoleColor.White);
            printf(@"+ ------ --=[ ", ConsoleColor.White, "0 build in commands         ", ConsoleColor.Gray  , "]\n", ConsoleColor.White);
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
