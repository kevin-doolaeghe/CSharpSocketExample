using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketClient {

    class Program {

        public static string Send(string message, string ip, int port)
        {
            string result = String.Empty;

            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, port);

                Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(ipEndpoint);

                    byte[] msg = Encoding.ASCII.GetBytes(message + "\r\n");
                    int bytesSent = socket.Send(msg);

                    byte[] bytes = new byte[1024];
                    int bytesRec = socket.Receive(bytes);

                    result = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (ArgumentNullException e)
                {
                    result += "ArgumentNullException : " + e.ToString();
                }
                catch (SocketException e)
                {
                    result += "SocketException : " + e.ToString();
                }
                catch (Exception e)
                {
                    result += "Unexpected exception : " + e.ToString();
                }
            }
            catch (Exception e)
            {
                result += "Exception : " + e.ToString();
            }

            return result;
        }

        public static int ReadInt()
        {
            try
            {
                string str = Console.ReadLine();
                return Convert.ToInt32(str);
            }
            catch
            {
                return ReadInt();
            }
        }

        private static void Main(String[] args)
        {
            Console.Write("Ip Adress: ");
            string ip = Console.ReadLine();

            Console.Write("Listening port: ");
            int port = ReadInt();

            Console.Write("Message: ");
            string message = Console.ReadLine();

            string result = Send(message, ip, port);
            Console.WriteLine("\nResponse :\n{0}", result);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }

}
