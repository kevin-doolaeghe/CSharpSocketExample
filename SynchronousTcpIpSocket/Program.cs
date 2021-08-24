using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SynchronousTcpIpSocket {
    
    class Program {

        private static void Main(String[] args)
        {
            string message = "GET / HTTP/1.1\r\n" +
                "Host: www.google.fr\r\n" +
                "\r\n";

            string ip = "www.google.fr";
            int port = 80;

            try
            {
                Console.WriteLine("1) Creating socket.");

                IPHostEntry ipHostInfo = Dns.GetHostEntry(ip);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    socket.Connect(remoteEP);
                    Console.WriteLine("Socket created to {0}\n", socket.RemoteEndPoint.ToString());

                    Console.WriteLine("2) Send packet.");

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(message);

                    // Send the data through the socket.
                    int bytesSent = socket.Send(msg);
                    Console.WriteLine("{0} bytes sent\n", bytesSent);

                    Console.WriteLine("3) Receive response.");

                    // Receive the response from the remote device.
                    byte[] bytes = new byte[1024];
                    int bytesRec = socket.Receive(bytes);

                    Console.WriteLine("Received {0} bytes :\n{1}", bytesRec, Encoding.ASCII.GetString(bytes));

                    // Release the socket.
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }

}
