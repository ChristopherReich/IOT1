using System;
using System.IO;
using System.Net;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace Http.Client
{ 
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("[Client] Start");

            // HTTP Client Objekt erzeugen
            var client = new HttpClient();

            ////Request 1 - Not Found

            //// Asynchron HTTP Anfrage abschicken (und nicht auf Antwort warten!)
            //var task1 = client.GetAsync("http://localhost:8081/");
            //// Main-Routine blockieren, bis die HTTP Antwort angekommen ist
            //task1.Wait();
            //// HTTP Antwort auslesen
            //var response1 = task1.Result;
            //// HTTP Antwort auf Konsole ausgeben
            //PrintResponse(response1);



            //// Request 2 - OK
            //var task2 = client.GetAsync("http://localhost:8081/a.html");
            //task2.Wait();
            //var response2 = task2.Result;
            //PrintResponse(response2);



            ////Request 3 - Custom Request

            ////Anfrageobjekt bauen
            //var request3 = new HttpRequestMessage();
            //request3.Method = HttpMethod.Post;
            //request3.RequestUri = new Uri("http://localhost:8081/b.html?param1=a&param2=b");
            //request3.Content = new StringContent("Spalte 1;Spalte 2;Spalte 3;", null, "text/csv");

            ////Anfrage senden und Antwort empfangen
            //var response3 = client.Send(request3); // Blockierende/synchrone Variante (ohne Task)!

            ////Antwort ausgeben
            //PrintResponse(response3);



            // -----------------------------------------------------------
            // Aufgabe 1 - Addition/Multiplikation/Division zweier Zahlen
            // -----------------------------------------------------------

            // HTTP Anfrage bauen
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri("http://localhost:8081/calc/");
            request.Content = new StringContent("10/0");

            // HTTP Anfrage senden und auf Antwort warten
            var sendTask = client.SendAsync(request);
            sendTask.Wait();

            var response = sendTask.Result;
            PrintResponse(response);


            // -----------------------------------------------------------
            // Aufgabe 2 - Vergleich zweier Zeichenketten
            // -----------------------------------------------------------

            // HTTP Anfrage bauen
            request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri("http://localhost:8081/compare/");
            request.Content = new StringContent("halloWelt;halloWelt");

            // HTTP Anfrage senden und auf Antwort warten
            sendTask = client.SendAsync(request);
            sendTask.Wait();

            response = sendTask.Result;
            PrintResponse(response);







            Console.WriteLine("[Client] End");
            Console.ReadLine();
        }

        private static void PrintResponse(HttpResponseMessage response)
        {
            //Status Code und Begründung ausgeben
            Console.WriteLine($"\n[Client] Response.StatusCode = {response.StatusCode}");
            Console.WriteLine($"[Client] Response.ReasonPhrase = {response.ReasonPhrase}");

            //Zusätzliche Header ausgeben
            foreach (var header in response.Headers)
            {
                var key = header.Key;
                foreach (var value in header.Value)
                {
                    Console.WriteLine($"[Client] Response.Headers[{key}] = {value}");
                }
            }


            //Body der String ausgeben
            var task = response.Content.ReadAsStringAsync();
            //Warten bis Body vollständig angekommen ist
            task.Wait();
            //Body auslesen
            var body = task.Result;
            //Body ausgeben
            Console.WriteLine($"[Client] Response.Content = {body}");

        }

    }
}
