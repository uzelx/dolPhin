using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolPhin.clean
{
    public class print
    {
        public static string printf(params object[] args)
        {
            for (int i = 0; i < args.Length; i += 2)
            {
                colorPrint(args[i].ToString(), (ConsoleColor)args[i + 1]);
            }
            return "";
        }

        public static void colorPrint(string str, ConsoleColor clr)
        {
            ConsoleColor oclr = Console.ForegroundColor;
            Console.ForegroundColor = clr;
            Console.Write(str);
            Console.ForegroundColor = oclr;
        }
    }
}
