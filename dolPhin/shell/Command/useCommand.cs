using static dolPhin.clean.print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace dolPhin.shell.Command
{
    public class useCommand
    {
        public static void clear()
        {
            Console.Clear();
        }

        public static void exit()
        {
            Environment.Exit(-1);
        }

        public static void run()
        {
            string LHOST, RHOST, LPORT;

            bool lhostVal = shell.dshell.variables.TryGetValue("lhost", out LHOST);
            bool rhostVal = shell.dshell.variables.TryGetValue("rhost", out RHOST);
            bool lportVal = shell.dshell.variables.TryGetValue("lport", out LPORT);

            if (lhostVal && rhostVal && lportVal)
            {
                int lport = 0;
                bool contin = true;
                try
                {
                    lport = int.Parse(LPORT);
                }
                catch
                {
                    printf("E: ", ConsoleColor.Red, "lport is not an int.\n", ConsoleColor.White);
                    contin = false;
                }

                if (contin)
                {
                    Command.cmds.reversetcp.reversetcpconnection.run(LHOST, RHOST, lport);
                }
            }
            else { 
                if (!lhostVal)
                {
                    printf("E: ", ConsoleColor.Red, "lhost is not set.\n", ConsoleColor.White);
                }

                if (!rhostVal)
                {
                    printf("E: ", ConsoleColor.Red, "rhost is not set.\n", ConsoleColor.White);
                }

                if (!lportVal)
                {
                    printf("E: ", ConsoleColor.Red, "lport is not set.\n", ConsoleColor.White);
                }
            }
        }

        public static void set(string commandInput)
        {
            dolPhin.shell.Command.cmds.setCommand.set(commandInput);
        }

        public static void get(string commandInput)
        {
            dolPhin.shell.Command.cmds.getCommand.get(commandInput);
        }

        public static void rem(string commandInput)
        {
            dolPhin.shell.Command.cmds.remCommand.rem(commandInput);
        }
    }
}
