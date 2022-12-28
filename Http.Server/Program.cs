using System;
using System.IO;
using System.Net;
using System.Net.Http;
using libStringAnalyzer;

namespace Http.Server
{
    internal class Program
    {
        //    // HTTP Server starten, Anfragen bearbeiten
        //    // ASP.NET wird üblicherweise verwendet
        //    // Wir nutzen aber Basis-API

 
        static void Main(string[] args)
        {
            Console.WriteLine("[Server] Start");

            // HTTP Server stellen und starten
            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/prefix/");
            listener.Prefixes.Add("http://localhost:8081/");
            listener.Start();

            // HTTP Anfragen verarbeiten, solange Server läuft
            while (listener.IsListening)
            {
                // Auf HTTP Anfrage warten
                var context = listener.GetContext();

                var request = context.Request;
                var response = context.Response;

                var input = request.InputStream;
                var output = response.OutputStream;

                var reader = new StreamReader(input);
                var writer = new StreamWriter(output);

                // Methode und Pfad der HTTP Anfrage auslesen
                Console.WriteLine($"[Server] Request.HttpMethod = {request.HttpMethod}");
                Console.WriteLine($"[Server] Request.Url = {request.Url}");

                // Header der HTTP Anfrage auslesen
                foreach (string key in request.Headers)
                {
                    var value = request.Headers[key];
                    Console.WriteLine($"[Server] Request.Headers[{key}] = {value}");
                }

                // Body der HTTP Anfrage auslesen
                var body = reader.ReadToEnd();
                Console.WriteLine($"[Server] Request.Content = {body}");

                // Status Code der HTTP Antwort setzen und Body der HTTP Antwort schreiben
                if (request.Url.AbsolutePath.StartsWith("/calc/"))
                {
                    if (request.HttpMethod.Equals(HttpMethod.Get.Method))
                    {
                        writer.Write("[Server] GET-Response: ");
                        var content = StringAnalyzer.Evaluate(body);
                        writer.Write(content);

                    }
                    else if (request.HttpMethod.Equals(HttpMethod.Post.Method))
                    {
                        writer.Write("Das ist Antwort A-POST.");
                    }
                    else if (request.HttpMethod.Equals(HttpMethod.Put.Method))
                    {
                        writer.Write("Das ist Antwort A-PUT.");
                    }
                    else if (request.HttpMethod.Equals(HttpMethod.Delete.Method))
                    {
                        writer.Write("Das ist Antwort A-DELETE.");
                    }
                }
                else if (request.Url.AbsolutePath.StartsWith("/compare/"))
                {
                    if (request.HttpMethod.Equals(HttpMethod.Get.Method))
                    {
                        writer.Write("Das ist Antwort A-GET.");
                    }
                    else if (request.HttpMethod.Equals(HttpMethod.Post.Method))
                    {
                        writer.Write("[Server] POST-Response: ");
                        var content = StringAnalyzer.Evaluate(body);
                        writer.Write(content);

                    }
                    else if (request.HttpMethod.Equals(HttpMethod.Put.Method))
                    {
                        writer.Write("Das ist Antwort B-PUT.");
                    }
                    else if (request.HttpMethod.Equals(HttpMethod.Delete.Method))
                    {
                        writer.Write("Das ist Antwort B-DELETE.");
                    }
                }
                else if (request.Url.AbsolutePath.StartsWith("/c/"))
                {
                    if (request.HttpMethod.Equals(HttpMethod.Get.Method))
                    {
                        writer.Write("Das ist Antwort C-GET.");
                    }
                    else if (request.HttpMethod.Equals(HttpMethod.Post.Method))
                    {
                        writer.Write("Das ist Antwort C-POST.");
                    }
                    else if (request.HttpMethod.Equals(HttpMethod.Put.Method))
                    {
                        writer.Write("Das ist Antwort C-PUT.");
                    }
                    else if (request.HttpMethod.Equals(HttpMethod.Delete.Method))
                    {
                        writer.Write("Das ist Antwort C-DELETE.");
                    }
                }
                else
                {
                    // Status Code der HTTP Antwort setzen
                    response.StatusCode = ((int)HttpStatusCode.NotFound);

                    writer.Write("Pfad nicht gefunden!");
                }
                writer.Flush();

                // HTTP Antwort senden
                response.Close();
            }

            // HTTP Server stoppen
            listener.Stop();

            // HTTP Server beenden
            Console.WriteLine("[Server] End");
            Console.ReadLine();
        }
    }
}
