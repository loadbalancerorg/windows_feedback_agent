using System;
using System.Net.Sockets;
using System.IO;

namespace SocketTest
{


    public class Client
    {
        static public void Main(string[] Args)
        {
            Console.WriteLine("Press a key (x to exit)");
            while ('x' != Console.ReadKey(true).KeyChar)
            {
                TcpClient socketForServer;
                try
                {
                    socketForServer = new TcpClient("localHost", 3333);
                }
                catch
                {
                    Console.WriteLine(
                        "Failed to connect to server at {0}:3333", "localhost");
                    return;
                }

                NetworkStream networkStream = socketForServer.GetStream();
                StreamReader streamReader = new StreamReader(networkStream);
                StreamWriter streamWriter = new StreamWriter(networkStream);

                try
                {
                    string outputString;

                    // read the data from the host and display it

                    {
                        outputString = streamReader.ReadLine();
                        Console.WriteLine(String.Format("Idle cpu {0}", outputString));
                        streamWriter.Flush();
                    }

                }
                catch
                {
                    Console.WriteLine("Exception reading from Server");
                }

                // tidy up
                networkStream.Close();
            }
        }
    }
}
