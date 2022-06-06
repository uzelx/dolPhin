using System;
using static dolPhin.clean.print;

namespace dolPhin
{
    public class Program
    {
        public static string tcpc = "cmd";
        public static void Main(string[] args)
        {
            var exitEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (sender, eventArgs) => {
                eventArgs.Cancel = true;
                exitEvent.Set();
            };

            Initialize.Run();

            while (true) 
            { 
                while (true)
                {
                    printf(shell.dshell.prompt());
                    string commandInput = Console.ReadLine();

                    string cmd = shell.CommandSwitcher.getCommand(commandInput);

                    bool newLine = true;
                    switch (cmd)
                    {
                        case "empty":
                            newLine = false;
                            break;
                        case "clear":
                            shell.Command.useCommand.clear();
                            break;
                        case "exit":
                            shell.Command.useCommand.exit();
                            break;
                        case "run":
                            shell.Command.useCommand.run();
                            break;

                        case "set":
                            shell.Command.useCommand.set(commandInput);
                            break;
                        case "get":
                            shell.Command.useCommand.get(commandInput);
                            break;
                        case "rem":
                            shell.Command.useCommand.rem(commandInput);
                            break;


                        case "unknown":
                            printf("E: ", ConsoleColor.Red, "Command not found.\n", ConsoleColor.White);
                            break;
                    }

                    if (newLine)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }
    }
}