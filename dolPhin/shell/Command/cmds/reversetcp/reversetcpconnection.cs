using static dolPhin.clean.print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace dolPhin.shell.Command.cmds.reversetcp
{
    public class reversetcpconnection
    {
        public static List<string> donotShow = new List<string>();
        public static void run(string lhost, string rhost, int lport)
        {
            try
            {
                bool validl = validIPV4(lhost);
                bool validr = validIPV4(rhost);
                bool validp = validPort(lport);

                if (!validr)
                {
                    if (rhost.Trim() == "*")
                    {
                        validr = true;
                    }
                }


                if (validl && validr && validp)
                {
                    IPAddress ipaddr = IPAddress.Parse(lhost);
                    IPEndPoint localEP = new IPEndPoint(ipaddr, lport);
                    TcpListener tcpListener = new TcpListener(localEP);
                    TcpClient tcpClient = null;

                    printf(" ____________________________________ \n", ConsoleColor.White);
                    printf("| ", ConsoleColor.White, "IPAddress", ConsoleColor.Green, "       | ", ConsoleColor.White, "Port", ConsoleColor.Blue, "  | ", ConsoleColor.White, "Time", ConsoleColor.Yellow, "     |\n", ConsoleColor.White);
                    printf("|-----------------|-------|----------|\n", ConsoleColor.White);

                    tcpListener.Start();
                    tcpClient = tcpListener.AcceptTcpClient();

                    while (((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString() != rhost)
                    {
                        if (rhost.Trim() != "*")
                        {
                            string rip = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
                            string rp = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port.ToString();

                            // print

                            if (!donotShow.Contains(rip))
                            {
                                printf("| ", ConsoleColor.White,
                                       $"{addspace(rip, 15)}", ConsoleColor.Green,
                                       " | ", ConsoleColor.White,
                                       $"{addspace(rp, 5)}", ConsoleColor.Blue,
                                       " | ", ConsoleColor.White,
                                       $"{DateTime.Now.ToString("HH:mm:ss")}", ConsoleColor.Yellow,
                                       " |\n", ConsoleColor.White);
                            }

                            if (!donotShow.Contains(rip))
                            {
                                donotShow.Add(rip);
                            }


                            // close connection
                            tcpListener.Stop();
                            tcpClient.GetStream().Close();
                            tcpClient.Close();
                            tcpListener.Start();
                            tcpClient = tcpListener.AcceptTcpClient();
                        }
                        else
                        {
                            break;
                        }
                    }

                    printf("| ", ConsoleColor.White,
                                       $"{addspace(((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString(), 15)}", ConsoleColor.Green,
                                       " | ", ConsoleColor.White,
                                       $"{addspace(((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port.ToString(), 5)}", ConsoleColor.Blue,
                                       " | ", ConsoleColor.White,
                                       $"{DateTime.Now.ToString("HH:mm:ss")}", ConsoleColor.Yellow,
                                       " |\n", ConsoleColor.White);
                    printf(" ------------------------------------ \n\n", ConsoleColor.White);

                    printf("[", ConsoleColor.White, "*", ConsoleColor.Blue, "] ", ConsoleColor.White, "Connection Recived.\n", ConsoleColor.White);
                    printf("[", ConsoleColor.White, "*", ConsoleColor.Blue, "] ", ConsoleColor.White, "Spawning Shell...\n\n", ConsoleColor.White);
                    Program.tcpc = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();

                    int index = 0;
                    string firstmessage = string.Empty;
                    while (true)
                    {
                        try
                        {
                            index++;
                            NetworkStream stream = tcpClient.GetStream();
                            
                            byte[] array = new byte[tcpClient.ReceiveBufferSize];

                            if (index != 1 && dshell.variables.ContainsKey("second") || !dshell.variables.ContainsKey("second"))
                            {
                                int num2 = stream.Read(array, 0, array.Length);// Math.Min(array.Length, tcpClient.ReceiveBufferSize));
                            }

                            string RecivedData = Encoding.ASCII.GetString(array);
                            RecivedData = RecivedData.Replace("\0", "");

                            try
                            {
                                RecivedData = RecivedData.Substring(0, RecivedData.Length - 1);
                            }
                            catch { /*skip*/ }

                            if (array.Length > 65536)
                            {
                                array = new byte[tcpClient.ReceiveBufferSize];
                            }
                            string Command = string.Empty;

                            if (index == 1)
                            {
                                firstmessage = RecivedData;
                                Console.Write(firstmessage + (dshell.variables.ContainsKey("cprompt") ? "\n\n" : ""));
                            }
                            else
                            {
                                Console.Write(RecivedData + (dshell.variables.ContainsKey("cprompt") ? "\n\n" : ""));
                            }

                            while (string.IsNullOrEmpty(Command.Trim()))
                            {
                                printf(shell.dshell.prompt());
                                Command = Console.ReadLine();
                            }

                            RecivedData = string.Empty;
                            byte[] bytes = Encoding.ASCII.GetBytes(Command);
                            stream.Write(bytes, 0, bytes.Length);
                            stream.Flush();
                        }
                        catch (Exception ex)
                        {
                            printf("E: ", ConsoleColor.Red, ex.Message + "\n", ConsoleColor.White);
                            Program.tcpc = "shell";
                            tcpListener.Stop();
                            tcpClient.GetStream().Close();
                            tcpClient.Close();
                            break;
                        }
                    }

                    Program.tcpc = "shell";
                }
                else
                {
                    if (!validl)
                    {
                        printf("E: ", ConsoleColor.Red, "lhost is not an valid IPAddress.\n", ConsoleColor.White);
                    }

                    if (!validr)
                    {
                        printf("E: ", ConsoleColor.Red, "rhost is not an valid IPAddress.\n", ConsoleColor.White);
                    }

                    if (!validp)
                    {
                        printf("E: ", ConsoleColor.Red, "lport is not an valid Port.\n", ConsoleColor.White);
                    }
                }
            }
            catch (Exception ex) {
                printf("E: ", ConsoleColor.Red, ex.Message, ConsoleColor.White);
            }
        }

        public static string addspace(string st, int max)
        {
            string result = st;
            for (int i = st.Length; i < max; i++)
            {
                result += " ";
            }
            return result;
        }

        public static bool validPort(int port)
        {
            if (port >= 1 && port <= 65535)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool validIPV4(string adddress)
        {
            adddress = adddress.Trim();
            if (adddress.Contains('.') &&
                adddress.Length <= 15 &&
                adddress.Length >= 7 &&
                IsDigitsOnly(adddress.Replace(".", "")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9') {
                    return false;
                }
            }
            return true;
        }
    }
}
