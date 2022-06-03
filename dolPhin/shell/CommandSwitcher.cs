using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolPhin.shell
{ 
    public class CommandSwitcher
    {
        public static string getCommand(string commandInput)
        {
            if (commandInput == null)
            {
                Console.Write("\n");
                return "empty";
            }

            string cleanInput = commandInput.ToLower().Trim();
            
            if (cleanInput=="")
            {
                return "empty";
            }
            else if (cleanInput=="clear")
            { 
                return "clear";
            }
            else if (cleanInput=="exit")
            {
                return "exit";
            }
            else if (cleanInput=="run")
            {
                return "run";
            }

            else if (cleanInput.StartsWith("set"))
            {
                return "set";
            }
            else if (cleanInput.StartsWith("get"))
            {
                return "get";
            }
            else if (cleanInput.StartsWith("rem"))
            {
                return "rem";
            }

            else
            {
                return "unknown";
            }
        }
    }
}
