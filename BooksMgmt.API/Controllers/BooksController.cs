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
    public class BooksController : ControllerBase
    {
        private readonly InMemoryData _data;

        public BooksController(InMemoryData data)
        {
            _data = data;
        }

        // GET api/Books?name=HP - Read
        // GET api/Books/5 - Read with Id
        // POST api/Books - Create 
        // DELETE api/Books - Delete
        // PUT api/Books - Update
        // GET api/Students/10/Batches/1

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ExceptionHandlingFilter]
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


        [HttpGet("{bookId}/Authors/{authorId}")]
        public ActionResult<Author> GetAuthor(int bookId, int authorId)
        {
            var books = _data.Books.FirstOrDefault(x => x.Id == bookId && x.Author.Id == authorId);

            if (books == null)
            {
                return NotFound("Book with given title not found");
            }

            return Ok(books);
        }


        [HttpPost]
        [ExceptionHandlingFilter]
        public IActionResult CreateBook(Book newBook)
        {
            throw new NotImplementedException("Not implemented");
            //_data.Books.Add(newBook);
            //return Ok(newBook);
        }


    }
}
