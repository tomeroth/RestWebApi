using AuthorManager.LiteDB.Model;
using ClassLibrary1;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorManager.LiteDB
{
    public class AuthorRepository
    {
        private readonly string _authorsConnection = DatabaseConnections.AuthorsConnection;

        public List<Author> GetAll()
        {
            using (var db = new LiteDatabase(this._authorsConnection))
            {
                var repository = db.GetCollection<AuthorDB>("authors");
                var results = repository.FindAll();

                return results.Select(x => Map(x)).ToList();
            }
        }

        public int Add(Author Author)
        {
            using (var db = new LiteDatabase(this._authorsConnection))
            {
                var dbObject = InverseMap(Author);

                var repository = db.GetCollection<AuthorDB>("authors");
                if (repository.FindById(Author.Id) != null)
                    repository.Update(dbObject);
                else
                    repository.Insert(dbObject);

                return dbObject.Id;
            }
        }

        public Author Get(int id)
        {
            using (var db = new LiteDatabase(this._authorsConnection))
            {
                var repository = db.GetCollection<AuthorDB>("authors");
                var result = repository.FindById(id);
                return Map(result);
            }
        }

        public Author Update(Author Author)
        {
            using (var db = new LiteDatabase(this._authorsConnection))
            {
                var dbObject = InverseMap(Author);

                var repository = db.GetCollection<AuthorDB>("authors");
                if (repository.Update(dbObject))
                    return Map(dbObject);
                else
                    return null;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(this._authorsConnection))
            {
                var repository = db.GetCollection<AuthorDB>("authors");
                return repository.Delete(id);
            }
        }

        internal List<AuthorDB> GetAuthors(int[] ids)
        {
            using (var db = new LiteDatabase(this._authorsConnection))
            {
                var repository = db.GetCollection<AuthorDB>("authors");
                var results = repository.FindAll().Where(x => ids.Contains(x.Id));

                return results.ToList();
            }
        }

        internal Author Map(AuthorDB author)
        {
            if (author == null)
                return null;
            return new Author() { Id = author.Id, AuthorName = author.AuthorName, AuthorSurname = author.AuthorSurname };
        }

        internal AuthorDB InverseMap(Author author)
        {
            if (author == null)
                return null;
            return new AuthorDB() { Id = author.Id, AuthorName = author.AuthorName, AuthorSurname = author.AuthorSurname };
        }
    }
}
