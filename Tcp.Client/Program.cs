using System;
using System.IO;
using System.Net.Cache;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Tcp.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[Client] Starting");

            var hostname = "127.0.0.1";
            var port = 3000;

            Console.WriteLine("[Client] Creating client");

            var client = new TcpClient(hostname, port);

            Console.WriteLine("[Client] Client created");

            var stream = client.GetStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var writer = new StreamWriter(stream, Encoding.UTF8);


            string request;
            string response;
            do
            {
                Console.Write("[Client] Anfrage: ");

                // Anfrage von Nutzer lesen
                request = Console.ReadLine();

                // Anfrage an Server schicken
                writer.WriteLine(request);
                writer.Flush();

                // Antwort von Server lesen
                response = reader.ReadLine();

                // Antwort an Nutzer ausgeben
                Console.WriteLine($"[Client] Antwort: {response}");
            }
            while (request != "exit" && response != "exit");

            Console.WriteLine("[Client] Closing client");

            client.Close();

            Console.WriteLine("[Client] closed");

            Thread.Sleep(1000);
        }
    }
}
