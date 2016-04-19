using BookManager.LiteDB.Model;
using LiteDB;
using System;
using ClassLibrary1;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.LiteDB
{
    public class BookRepository
    {
        private readonly string _booksConnection = DatabaseConnections.BooksConnection;

        public List<Book> GetAll()
        {
            using (var db = new LiteDatabase(this._booksConnection))
            {
                var repository = db.GetCollection<BookDB>("books");
                var results = repository.FindAll();

                return results.Select(x => Map(x)).ToList();
            }
        }

        public int Add(Book book)
        {
            using (var db = new LiteDatabase(this._booksConnection))
            {
                var dbObject = InverseMap(book);

                var repository = db.GetCollection<BookDB>("books");
                if (repository.FindById(book.Id) != null)
                    repository.Update(dbObject);
                else
                    repository.Insert(dbObject);

                return dbObject.Id;
            }
        }

        public Book Get(int id)
        {
            using (var db = new LiteDatabase(this._booksConnection))
            {
                var repository = db.GetCollection<BookDB>("books");
                var result = repository.FindById(id);
                return Map(result);
            }
        }

        public Book Update(Book book)
        {
            using (var db = new LiteDatabase(this._booksConnection))
            {
                var dbObject = InverseMap(book);

                var repository = db.GetCollection<BookDB>("books");
                if (repository.Update(dbObject))
                    return Map(dbObject);
                else
                    return null;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(this._booksConnection))
            {
                var repository = db.GetCollection<BookDB>("books");
                return repository.Delete(id);
            }
        }

        internal List<BookDB> GetBooks(int[] ids)
        {
            using (var db = new LiteDatabase(this._booksConnection))
            {
                var repository = db.GetCollection<BookDB>("books");
                var results = repository.FindAll().Where(x => ids.Contains(x.Id));

                return results.ToList();
            }
        }

        internal Book Map(BookDB dbBook)
        {
            if (dbBook == null)
                return null;
            return new Book() { Id = dbBook.Id, BookTitle = dbBook.BookTitle, ISBN = dbBook.ISBN };
        }

        internal BookDB InverseMap(Book book)
        {
            if (book == null)
                return null;
            return new BookDB() { Id = book.Id, BookTitle = book.BookTitle, ISBN = book.ISBN };
        }
    }
}
