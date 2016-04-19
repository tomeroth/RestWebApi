using AuthorManager.LiteDB;
using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    public class AuthorsController : ApiController
    {
        AuthorRepository authorRepository = new AuthorRepository();

        // GET api/authors/
        public IEnumerable<Author> Get()
        {
            return authorRepository.GetAll();
        }


        // GET api/authors/1
        public Author Get(int id)
        {
            return authorRepository.Get(id);
        }

        // POST api/authors
        public void Post([FromBody]Author author)
        {
            int i = 10;
        }

        // PUT api/authors/5
        public void Put(int id, [FromBody]Author author)
        {
            authorRepository.Add(author);
        }

        // DELETE api/authors/5
        public void Delete(int id)
        {
            authorRepository.Delete(id);
        }
    }
}
