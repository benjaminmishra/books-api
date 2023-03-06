using System.Data;
using BooksMgmt.Data.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Extensions.Logging;

namespace BooksMgmt.Data.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly string connectionString = "Data Source=localhost;Initial Catalog=booksdb;User Id=sa;Password=p@ssw0rd!!;MultipleActiveResultSets=True";
        private readonly ILogger<BooksRepository> _logger;

        public BooksRepository(ILogger<BooksRepository> logger)
        {
            _logger = logger;
        }

        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetAllBooks()
        {
            List<Book> books = new();

            try
            {
                await using SqlConnection conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                var selectBookCmd =
                    new SqlCommand("select Id, Title, Description, AuthorId, ISBN, GENRE from dbo.Books;", conn);
                var bookReader = await selectBookCmd.ExecuteReaderAsync();

                while (await bookReader.ReadAsync())
                {
                    var book = new Book
                    {
                        Title = bookReader["Title"].ToString() ?? string.Empty,
                        Description = bookReader["Description"].ToString() ?? string.Empty,
                        Genre = bookReader["Genre"].ToString() ?? string.Empty,
                        ISBN = bookReader["ISBN"].ToString() ?? string.Empty
                    };

                    var selectAuthorCmd =
                        new SqlCommand("select Name,Gender,DateofBirth from dbo.Authors where Id=@authorid;", conn);
                    selectAuthorCmd.Parameters.Add("@authorid", SqlDbType.Int, (int)bookReader["AuthorId"]);

                    var authorReader = await selectAuthorCmd.ExecuteReaderAsync();
                    await authorReader.ReadAsync();

                    var author = new Author()
                    {
                        Name = authorReader["Name"].ToString() ?? string.Empty,
                        Gender = authorReader["Gender"].ToString() ?? string.Empty,
                        DateOfBirth = DateTime.Parse(authorReader["DateofBirth"].ToString() ?? string.Empty)
                    };

                    book.Author = author;

                    books.Add(book);
                }
            }
            catch (Exception ex)
            {
                // log error
                _logger.Log(LogLevel.Error, ex.Message);
            }

            return books;
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