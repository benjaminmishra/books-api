using BooksMgmt.API.Dto;
using BooksMgmt.API.Filters;
using BooksMgmt.Data.Models;
using BooksMgmt.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksMgmt.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Basic", Roles = "Admin")]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;
    private readonly InMemoryData _data;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBooksRepository booksRepository, ILogger<BooksController> logger, InMemoryData data)
    {
        _booksRepository = booksRepository;
        _logger = logger;
        _data = data;
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ExceptionHandlingFilter]
    [Authorize(Roles = "Admin,User")]
    public ActionResult<Book> GetBook(int id)
    {
        var book = _data.Books.FirstOrDefault(x => x.Id == id);

        if (book == null)
        {
            return NotFound("Nothing found");
        }

        return Ok(book);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ExceptionHandlingFilter]
    public async Task<ActionResult<List<Book>>> GetAllBooks()
    {
        var books = await _booksRepository.GetAllBooks();
        return Ok(books);
    }


    [HttpGet("{bookId}/Authors/{authorId}")]
    public ActionResult<Author> GetAuthor(int bookId, int authorId)
    {
        var author = _data.Books.FirstOrDefault(x => x.Id == bookId && x.Author.Id == authorId)?.Author;

        if (author == null)
        {
            return NotFound("Author not found");
        }

        return Ok(author);
    }


    [HttpPost]
    [ExceptionHandlingFilter]
    public IActionResult CreateBook(Book newBook)
    {
        _data.Books.Add(newBook);
        return Ok(newBook);
    }

    [HttpPatch("{id}")]
    [ExceptionHandlingFilter]
    public IActionResult UpdateBook(BookUpdateRequest book,int id)
    {
        var bookToUpdate = _data.Books.FirstOrDefault(x=>x.Id==id);

        if (bookToUpdate == null)
        {
            return NotFound("Book with given id not found");
        }

        // update value , this partially updates the object
        bookToUpdate.Title = book.Title;
        bookToUpdate.Description = book.Description;
        bookToUpdate.Genre = book.Genre;
        bookToUpdate.ISBN = book.ISBN;

        return Ok(bookToUpdate);
    }


}