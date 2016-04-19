using BookManager.LiteDB;
using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    public class BooksController : ApiController
    {
        BookRepository bookRepository = new BookRepository();

        // GET api/books/
        public IEnumerable<Book> Get()
        {
            return bookRepository.GetAll();
        }

        // GET api/books?search=search_query
        public IEnumerable<Book> Get([FromUri] string search_query)
        {
            return bookRepository.GetAll().Where(x => x.BookTitle.Contains(search_query));
        }

        // GET api/books/1
        public Book Get(int id)
        {
            return bookRepository.Get(id);
        }

        // POST api/authors
        public void Post([FromBody]Book book)
        {
            int i = 10;
        }

        // PUT api/authors/5
        public void Put(int id, [FromBody]Book book)
        {
            bookRepository.Add(book);
        }

        // DELETE api/authors/5
        public void Delete(int id)
        {
            bookRepository.Delete(id);
        }
    }
    
}
