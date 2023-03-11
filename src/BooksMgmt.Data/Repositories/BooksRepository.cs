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
        _connStr = configuration.GetConnectionString("SqlServer");
    }

    public async Task AddBook(Book book)
    {
        await using SqlConnection conn = new SqlConnection(_connStr);
        await conn.OpenAsync();

        await using SqlTransaction transaction = conn.BeginTransaction();
        try
        {
            // BEGIN TRANSACTION
            var insertCmd =
                new SqlCommand("INSERT INTO dbo.Books (Title, Description, AuthorId, ISBN, GENRE) " +
                               "VALUES(@title,@desc,@authorId,@isbn,@gen)", conn);

            var insertAuthor =
                new SqlCommand("INSERT INTO dbo.Authors (Name, DateOfBirth, Gender) " +
                               "VALUES(@name,@dob,@gender)", conn);

            insertCmd.Parameters.Add(new SqlParameter("@title", book.Title));
            insertCmd.Parameters.Add(new SqlParameter("@desc", book.Description));
            insertCmd.Parameters.Add(new SqlParameter("@authorId", book.Author.Id));
            insertCmd.Parameters.Add(new SqlParameter("@isbn", book.Isbn));
            insertCmd.Parameters.Add(new SqlParameter("@gen", book.Genre));

            insertAuthor.Parameters.Add(new SqlParameter("@name", book.Author.Name));
            insertAuthor.Parameters.Add(new SqlParameter("@dob", book.Author.DateOfBirth));
            insertAuthor.Parameters.Add(new SqlParameter("@gender", book.Author.Gender));

            await insertAuthor.ExecuteNonQueryAsync();
            await insertCmd.ExecuteNonQueryAsync();

            await transaction.CommitAsync();
            //COMMIT
        }
        catch
        {
            // ROLLBACK
            await transaction.RollbackAsync();
        }
        // END
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
            SqlDataReader bookReader = await selectBookCmd.ExecuteReaderAsync();

            while (await bookReader.ReadAsync())
            {
                #region FillBookObj
                var book = new Book
                {
                    Title = bookReader["Title"].ToString() ?? string.Empty,
                    Description = bookReader["Description"].ToString() ?? string.Empty,
                    Genre = bookReader["Genre"].ToString() ?? string.Empty,
                    Isbn = bookReader["ISBN"].ToString() ?? string.Empty
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
#endregion
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
        DataSet dataSet = new DataSet();

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


            var booksTable = dataSet.Tables[0].AsEnumerable();
            var authorTable = dataSet.Tables[1].AsEnumerable();


            var bookQuery = from bookRow in booksTable
                            join authorRow in authorTable
                                on bookRow["AuthorId"] equals authorRow["Id"]
                            where (int)bookRow["Id"] == id && (int)authorRow["Id"] == (int)bookRow["AuthorId"]
                            select new Book
                            {
                                Id = (int)bookRow["Id"],
                                Description = (string)bookRow["Description"],
                                Genre = (string)bookRow["Genre"],
                                Isbn = (string)bookRow["ISBN"],
                                Title = (string)bookRow["Title"],
                                Author = new Author
                                {
                                    Name = (string)authorRow["Name"],
                                    DateOfBirth = (DateTime)authorRow["DateofBirth"],
                                    Gender = (string)authorRow["Gender"],
                                    Id = (int)authorRow["Id"]
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