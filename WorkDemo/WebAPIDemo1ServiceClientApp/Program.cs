using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WebAPIDemo1.Models;

namespace WebAPIDemo1ServiceClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client app, wait for service");
            Console.ReadLine();
           ReadArraySample().Wait();
            // ReadSampleXml().Wait();
            // AddSample().Wait();
            // UpdateSample().Wait();
            // DeleteSample().Wait();
            //UpdateWithErrorSample().Wait();
            Console.ReadLine();
        }

        private static async Task ReadArraySample()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:11825");
            string response = await client.GetStringAsync("/api/BookChapters");
            Console.WriteLine(response);
            var serializer = new JavaScriptSerializer();
            BookChapter[] chapters =
              serializer.Deserialize<BookChapter[]>(response);

            foreach (BookChapter chapter in chapters)
            {
                Console.WriteLine(chapter.Title);
            }
        }
    }
}
