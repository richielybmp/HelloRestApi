using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HelloRestApi
{
    public class Program
    {
        private const string URL = "https://jsonplaceholder.typicode.com/comments";
        private const string PARAMETROS = "";

        static void Main(string[] args)
        {
            Program.PrepararRestAPI();
        }

        private static void PrepararRestAPI()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(PARAMETROS).Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var comentarios = response.Content.ReadAsAsync<IEnumerable<Comentario>>().Result;
                Console.WriteLine("Listando items consumidos");
                foreach (var comentario in comentarios)
                {
                    Console.WriteLine("PostId: {0}", comentario.postId);
                    Console.WriteLine("Id: {0}", comentario.id);
                    Console.WriteLine("Nome: {0}", comentario.name);
                    Console.WriteLine("Email: {0}", comentario.email);
                    Console.WriteLine("Body: {0}", comentario.body);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.ReadLine();
        }
    }
}
