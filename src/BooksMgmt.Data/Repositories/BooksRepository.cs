using System.Data;
using BooksMgmt.Data.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace BooksMgmt.Data.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly ILogger<BooksRepository> _logger;
    private readonly string? _connStr;

    public BooksRepository(ILogger<BooksRepository> logger, IConfiguration configuration)
    {
        _logger = logger;
        _connStr = configuration.GetConnectionString("SqlServer") ?? throw new Exception("ConnectionString section is either empty or does not exist");
    }

    public void AddBook(Book book)
    {
        throw new NotImplementedException();
    }

    public void DeleteBook(int id)
    {
        throw new NotImplementedException();
    }


    // Connected model
    public async Task<List<Book>> GetAllBooks()
    {
        List<Book> books = new();

        try
        {
            await using SqlConnection conn = new SqlConnection(_connStr);
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
                selectAuthorCmd.Parameters.Add(new SqlParameter("@authorid", (int)bookReader["AuthorId"]));

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


    // Disconnected Model
    public async Task<Book?> GetBookById(int id)
    {
        Book? book = new();
        var dataSet = new DataSet();

        string selectStatments =
            @"select Id, Title, Description, AuthorId, ISBN, GENRE from dbo.Books where Id=@bookid;
select Id,Name,Gender,DateofBirth from dbo.Authors;";


        try
        {
            await using (SqlConnection conn = new SqlConnection(_connStr))
            {
                var command = new SqlCommand(selectStatments, conn);
                command.Parameters.Add(new SqlParameter("@bookid", id));

                var adapter = new SqlDataAdapter(command);

                var result = adapter.Fill(dataSet);

                if (result == 0) return default; // nothing added to DataSet just return null
            }

            var booksTable = dataSet.Tables[0];
            var authorTable = dataSet.Tables[1];

            var bookQuery = from bookRow in booksTable.AsEnumerable()
                            join authorRow in authorTable.AsEnumerable()
                                on bookRow["AuthorId"] equals authorRow["Id"]
                            where (int)bookRow["Id"] == id && (int)authorRow["Id"] == (int)bookRow["AuthorId"]
                            select new Book
                            {
                                Id = (int)bookRow["Id"],
                                Description = (string)bookRow["Description"],
                                Genre = (string)bookRow["Genre"],
                                ISBN = (string)bookRow["ISBN"],
                                Title = (string)bookRow["Title"],
                                Author = new Author
                                {
                                    Id = (int)authorRow["Id"],
                                    DateOfBirth = (DateTime)authorRow["DateofBirth"],
                                    Gender = (string)authorRow["Gender"],
                                    Name = (string)authorRow["Name"]
                                }
                            };

            book = bookQuery.FirstOrDefault();

        }
        catch (Exception ex)
        {
            // log error
            _logger.Log(LogLevel.Error, ex.Message);
        }

        return book;
    }

    public void UpdateBook(Book book)
    {
        throw new NotImplementedException();
    }
}