using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolPhin.shell.Command.cmds
{
    public class setCommand
    {
        public static void set(string commandInput)
        {
            commandInput = commandInput.Trim();
            string variableName = commandInput.Split(' ')[1];
            string variableValue = (((commandInput.Substring(3)).Trim()).Substring(variableName.Length)).Trim();

            shell.dshell.variables.Remove(variableName);
            shell.dshell.variables.Add(variableName, variableValue);
            Console.WriteLine(variableName + " ==> " + variableValue);
        }
    }
}
