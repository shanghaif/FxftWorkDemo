using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo1.Models;

namespace WebAPIDemo1.Controllers
{
    public class BookChaptersController : ApiController
    {
        private static List<BookChapter> chapters;
        static BookChaptersController()
        {
            chapters = new List<BookChapter>()
      {
        new BookChapter { Number=1, Title=".NET Architecture", Pages=20},
        new BookChapter { Number=2, Title="Core C#", Pages=42},
        new BookChapter { Number=3, Title="Objects and Types", Pages=24},
        new BookChapter { Number=4, Title="Inheritance", Pages=18},
        new BookChapter { Number=5, Title="Generics", Pages=22},
        new BookChapter { Number=17, Title="Visual Studio 2012", Pages=50},
        new BookChapter { Number=42, Title="ASP.NET Dynamic Data",
          Pages=14}
      };
        }

        // GET: api/BookChapters
        public IEnumerable<BookChapter> GetBookChapters()
        {
            return chapters;
        }
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}


        // GET: api/BookChapters/5
        public BookChapter GetBookChapter(int id)
        {
            return chapters.Where(c => c.Number == id).SingleOrDefault();
        }
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/BookChapters
        public void PostBookChapter([FromBody]BookChapter value)
        {
            chapters.Add(value);
        }
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/BookChapters/5
        public IHttpActionResult PutBookChapter(int id, [FromBody]BookChapter value)
        {
            try
            {
                chapters.Remove(chapters.Where(c => c.Number == id).Single());
                chapters.Add(value);
                return Ok();
            }
            catch (InvalidOperationException)  // chapter does not exist
            {
                return BadRequest();
            }
        }
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/BookChapters/5
        public void DeleteBookChapter(int id)
        {
            chapters.Remove(chapters.Where(c => c.Number == id).Single());
        }
        //public void Delete(int id)
        //{
        //}
    }
}
