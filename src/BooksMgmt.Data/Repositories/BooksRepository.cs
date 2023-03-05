using BooksMgmt.Data.Models;

namespace BooksMgmt.Data.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly string connectionString = "Data Source=localhost:1433;Initial Catalog=booksdb;User Id=sa;Password=p@ssw0rd!!";

        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Book GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}