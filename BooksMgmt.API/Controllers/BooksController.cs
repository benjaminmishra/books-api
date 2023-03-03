using System.Security.Cryptography.X509Certificates;
using BooksMgmt.API.Filters;
using BooksMgmt.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksMgmt.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly InMemoryData _data;

        public BooksController(InMemoryData data)
        {
            _data = data;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExceptionHandlingFilter]
        [Authorize(Roles = "Admin,User")]
        public ActionResult<Book> GetBooks(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot be less than or equal to zero");

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
        [Authorize(Roles = "User")]
        public ActionResult<Book> GetAllBooks()
        {
            return Ok(_data.Books);
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
        [Authorize(Roles="Admin")]
        public IActionResult CreateBook(Book newBook)
        {
            _data.Books.Add(newBook);
            return Ok(newBook);
        }

        [HttpPatch("{id}")]
        [ExceptionHandlingFilter]
        [Authorize(Roles = "Admin")]
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
}
