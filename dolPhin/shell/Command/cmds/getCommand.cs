using static dolPhin.clean.print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolPhin.shell.Command.cmds
{
    public class getCommand
    {
        public static void get(string commandInput)
        {
            commandInput = commandInput.Trim();
            string variableName = commandInput.Split(' ')[1];
            if (shell.dshell.variables.ContainsKey(variableName))
            {
                string variableValue;
                shell.dshell.variables.TryGetValue(variableName, out variableValue);

                Console.WriteLine(variableName + " ==> " + variableValue);
            }
            else
            {
                printf("E: ", ConsoleColor.Red, "Variable not set.", ConsoleColor.White);
            }            
        }
    }
}
