using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static dolPhin.clean.print;

namespace dolPhin.shell.Command.cmds
{
    public class remCommand
    {
        public static void rem(string commandInput)
        {
            commandInput = commandInput.Trim();
            string variableName = commandInput.Split(' ')[1];
            if (shell.dshell.variables.ContainsKey(variableName))
            {
                string variableValue;
                shell.dshell.variables.TryGetValue(variableName, out variableValue);
                Console.WriteLine(variableName + " ==> " + variableValue);

                try
                {
                    shell.dshell.variables.Remove(variableName);
                    printf("[+] ", ConsoleColor.Green, "Variable was Successfully removed.\n", ConsoleColor.White);
                }
                catch (Exception ex) {
                    printf("E: ", ConsoleColor.Red, ex.Message+"\n", ConsoleColor.White);
                }
            }
            else
            {
                printf("E: ", ConsoleColor.Red, "Variable not set.", ConsoleColor.White);
            }
        }
    }
}
