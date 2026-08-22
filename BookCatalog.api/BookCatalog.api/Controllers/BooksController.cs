using BookCatalog.api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        private static List<Book> books = new List<Book>();

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            _logger.LogInformation("Retrieved all books. Count: {Count}", books.Count);
            return books;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                _logger.LogWarning("Book with Id {Id} was not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Book with Id {Id} retrieved successfully.", id);
            return book;
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            try
            {
                book.Id = books.Count + 1;
                books.Add(book);
                _logger.LogInformation("Book created successfully. Id: {Id}", book.Id);
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            
            catch (Exception ex)
            {
                _logger.LogError( ex,"An error occurred while creating the book.");
                 return StatusCode(StatusCodes.Status500InternalServerError,"An unexpected error occurred while creating the book.");
            }
        }

            
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public ActionResult<Book> UpdateBook(int id, Book updatedBook)
        {
            try
            {
                var book = books.FirstOrDefault(b => b.Id == id);

                if (book == null)
                {
                    _logger.LogWarning("Book with Id {Id} was not found for update.", id);
                    return NotFound();
                }

                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.PublishedYear = updatedBook.PublishedYear;
                _logger.LogInformation("Book with Id {Id} updated successfully.", id);
                return book;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book with Id {Id}.", id);
                 return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while updating the book.");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var book = books.FirstOrDefault(b => b.Id == id);

                if (book == null)
                {

                    _logger.LogWarning("Book with Id {Id} was not found for deletion.", id);
                    return NotFound();
                }

                books.Remove(book);
                _logger.LogInformation("Book with Id {Id} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book with Id {Id}.", id);

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while deleting the book.");
            }
        }
    }
}
