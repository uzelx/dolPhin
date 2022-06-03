using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolPhin.shell 
{ 
    public class dshell
    {
        public static Dictionary<string, string> variables = new Dictionary<string, string>();

        public static object[] prompt()
        {
            if (Program.tcpc != "shell")
            {
                if (variables.ContainsKey("cprompt"))
                {
                    object[] prompt = {
                        "dolphin tcp(", ConsoleColor.White,
                        Program.tcpc, ConsoleColor.DarkYellow,
                        ") > ", ConsoleColor.White
                    };
                    return prompt;
                }
                else
                {
                    object[] prompt = {
                        "", ConsoleColor.White
                    };
                    return prompt;
                }
            }
            else
            {
                object[] prompt = {
                        "dolphin tcp(", ConsoleColor.White,
                        Program.tcpc, ConsoleColor.DarkYellow,
                        ") > ", ConsoleColor.White
                    };
                return prompt;
            }
        }
    }
}
