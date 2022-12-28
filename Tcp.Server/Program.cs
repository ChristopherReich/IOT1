using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using libStringAnalyzer;

namespace Tcp.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[Server] Starting");

            var address = IPAddress.Parse("127.0.0.1");
            int port = 3000;

            Console.WriteLine("[Server] Create listener");

            var listener = new TcpListener(address, port);

            Console.WriteLine("[Server] Start listener");

            listener.Start();

            Console.WriteLine("[Server] Accept client");

            var client = listener.AcceptTcpClient();

            Console.WriteLine("[Server] Client accepted");

            var stream = client.GetStream();

            //var data = new byte[3];

            //stream.Write();
            //stream.Read(data, 0, 3);

            //Console.WriteLine($"[Server] Data[0] = {data[0]}");
            //Console.WriteLine($"[Server] Data[1] = {data[1]}");
            //Console.WriteLine($"[Server] Data[2] = {data[2]}");

            var reader = new StreamReader(stream, Encoding.UTF8);
            var writer = new StreamWriter(stream, Encoding.UTF8);


            //Console.WriteLine($"[Server] line = {reader.ReadLine()}");
            //Console.WriteLine($"[Server] line = {reader.ReadLine()}");

            string request;
            string response;
            do
            {
                // Anfrage des Client schicken
                request = reader.ReadLine();

                // Anfrage an Nutzer ausgeben
                Console.WriteLine($"[Server] Anfrage: {request}");

                // Antwort berechnen
                if (!request.Contains("exit"))
                {
                    response = StringAnalyzer.Evaluate(request);
                    // Antwort an Nutzer ausgeben
                    Console.WriteLine($"[Server] Antwort: {response}");       
                }
                else
                {
                    response = "Exit acknowledged";
                    // Antwort an Nutzer ausgeben
                    Console.WriteLine($"[Server] Antwort: {response}");
                }

                // Antwort an Client schicken
                writer.WriteLine(response);
                writer.Flush();
            }
            while (request != "exit" && response != "exit");

            Console.WriteLine("[Server] Closing client");

            client.Close();

            Console.WriteLine("[Server] closed");

            Thread.Sleep(1000);

        }
    }
}
