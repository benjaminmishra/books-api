using BooksMgmt.API.Dto;
using BooksMgmt.API.Filters;
using BooksMgmt.Data;
using BooksMgmt.Data.Models;
using BooksMgmt.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksMgmt.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Basic")]
public class BooksController : ControllerBase
{
    private readonly InMemoryData _data;
    private readonly ILogger<BooksController> _logger;
    private readonly BooksDbContext _booksDbContext;

    public BooksController(ILogger<BooksController> logger, InMemoryData data, BooksDbContext context)
    {
        _booksDbContext = context;
        _logger = logger;
        _data = data;
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ExceptionHandlingFilter]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var bookQuery = from book in _booksDbContext.Books
                        where book.Id == id
                        select book;

        return Ok(await bookQuery.Include(x => x.Author).ToListAsync());
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ExceptionHandlingFilter]
    public async Task<ActionResult<List<Book>>> GetAllBooks([FromQuery] string? genre, [FromQuery] string? title)
    {
        var booksQuery = _booksDbContext.Books.Include(b => b.Author);

        if (genre != null)
            booksQuery.Where(x => x.Genre == genre);

        if (title != null)
            booksQuery.Where(x => x.Title.Contains(title));


        var book = await booksQuery.ToListAsync();

        return Ok(book);
    }


    [HttpGet("{bookId}/Authors/{authorId}")]
    public ActionResult<Author> GetAuthor(int bookId, int authorId)
    {
        //var author = _data.Books.FirstOrDefault(x => x.Id == bookId && x.Author.Id == authorId)?.Author;

        //if (author == null)
        //{
        //    return NotFound("Author not found");
        //}

        //return Ok(author);

        return Ok();
    }


    [HttpPost]
    [ExceptionHandlingFilter]
    public IActionResult CreateBook(BookCreateRequest createBookReq)
    {
        Book book = new Book()
        {
            Title = createBookReq.Title,
            Description = createBookReq.Description,
            Genre = createBookReq.Genre,
            Author = new Author()
            {

            }
        };
        return Ok(book.Id);
    }

    [HttpPatch("{id}")]
    [ExceptionHandlingFilter]
    public IActionResult UpdateBook(BookUpdateRequest book, int id)
    {
        var bookToUpdate = _data.Books.FirstOrDefault(x => x.Id == id);

        if (bookToUpdate == null)
        {
            return NotFound("Book with given id not found");
        }

        // update value , this partially updates the object
        bookToUpdate.Title = book.Title;
        bookToUpdate.Description = book.Description;
        bookToUpdate.Genre = book.Genre;
        bookToUpdate.Isbn = book.ISBN;

        return Ok(bookToUpdate);
    }

    [HttpDelete("{id}")]
    [ExceptionHandlingFilter]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var bookQuery = _booksDbContext.Books.Include(b => b.Author).Where(x => x.Id == id);

        var book = await bookQuery.FirstOrDefaultAsync();

        if (book == null)
        {
            return NotFound();
        }

        _booksDbContext.Books.Remove(book);
        _booksDbContext.Authors.Remove(book.Author);

        await _booksDbContext.SaveChangesAsync();

        return Ok();
    }
}