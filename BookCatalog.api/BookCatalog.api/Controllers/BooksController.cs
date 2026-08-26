using BookCatalog.Common.Interfaces.Services;
using BookCatalog.Common.Models.Dtos.Requests;
using BookCatalog.Common.Models.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<BookResponse>> GetBooks()
        {
            var books = _bookService.GetAll();
            _logger.LogInformation("Retrieved all books. Count: {Count}", books.Count);
            return books;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<BookResponse> GetBook(int id)
        {
            var book = _bookService.Get(id);

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
        public ActionResult<BookResponse> CreateBook(CreateBookRequest bookRequest)
        {
            try
            {
                var bookResponse = _bookService.Create(bookRequest);
                _logger.LogInformation("Book created successfully. Id: {Id}", bookResponse.Id);
                return CreatedAtAction(nameof(GetBook), new { id = bookResponse.Id }, bookResponse);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the book.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while creating the book.");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut()]
        public ActionResult<BookResponse> UpdateBook(UpdateBookRequest updateBook)
        {
            try
            {
                var bookResponse = _bookService.Update(updateBook);

                if (bookResponse == null)
                {
                    _logger.LogWarning("Book with Id {Id} was not found for update.", updateBook.Id);
                    return NotFound();
                }

                _logger.LogInformation("Book with Id {Id} updated successfully.", updateBook.Id);
                return bookResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book with Id {Id}.", updateBook.Id);
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
                var result = _bookService.Delete(id);

                if (!result)
                {

                    _logger.LogWarning("Book with Id {Id} was not found for deletion.", id);
                    return NotFound();
                }

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
