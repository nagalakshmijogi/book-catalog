using BookCatalog.Common.Interfaces.Repositories;
using BookCatalog.Common.Models.DataModels;
using BookCatalog.Common.Models.Dtos.Requests;
using BookCatalog.Common.Models.Dtos.Responses;

namespace BookCatalog.BusinessLogicLayer.Repositories
{
    public class InMemoryBookRepository : IBookRepository
    {
        private List<Book> _books;
        private int _currentId = 0;
        public InMemoryBookRepository()
        {
            _books = new List<Book>();
        }
        public BookResponse Create(CreateBookRequest request)
        {
            _currentId++;
            _books.Add(
                new Book()
                {
                    Id = _currentId,
                    Title = request.Title,
                    PublishedYear = request.PublishedYear,
                    Author = request.Author
                });
            return new BookResponse()
            {
                Id = _currentId,
                Title = request.Title,
                PublishedYear = request.PublishedYear,
                Author = request.Author
            };
        }
        public bool Delete(int id)
        {
            var book = _books.Find(x => x.Id == id);
            if (book == null)
            {
                return false;
            }
            return _books.Remove(book);
        }
        public BookResponse? Get(int id)
        {
            var book = _books.Find(x => x.Id == id);
            if (book == null)
            {
                return null;
            }
            return new BookResponse()
            {
                Id = book.Id,
                Author = book.Author,
                PublishedYear = book.PublishedYear,
                Title = book.Title,
            };
        }
        public PagedResult<BookResponse> GetAll(BookFilter filter)
{
            var books = _books.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                books = books.Where(book => 
                        book.Title.Contains( filter.Title,
                        StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(filter.Author))
            {
                books = books.Where(book =>
                        book.Author.Contains( filter.Author,
                        StringComparison.OrdinalIgnoreCase));
            }
            if (filter.PublishedYear.HasValue)
            {
                books = books.Where(book =>
                        book.PublishedYear == filter.PublishedYear.Value);
            }
            var totalCount = books.Count();
            var totalPages = 0;
            if((totalCount % filter.PageSize) == 0)
            {
                totalPages = (totalCount / filter.PageSize);
            }
            else
            {
                totalPages = (totalCount / filter.PageSize) + 1;
            }
            var pagedBooks = books
                .Skip((filter.PageNumber - 1) * filter.PageSize) 
                .Take(filter.PageSize)
                .Select(book => new BookResponse()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    PublishedYear = book.PublishedYear
                })
                .ToList();
            return new PagedResult<BookResponse>
            {
                Items = pagedBooks,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }
        public BookResponse? Update(UpdateBookRequest request)
        {
            var book = _books.Find(x => x.Id == request.Id);
            if (book == null)
            {
                return null;
            }
            _books.Remove(book);
            _books.Add(
                new Book()
                {
                    Id = request.Id,
                    Title = request.Title,
                    PublishedYear = request.PublishedYear,
                    Author = request.Author
                });
            return new BookResponse()
            {
                Id = request.Id,
                Title = request.Title,
                PublishedYear = request.PublishedYear,
                Author = request.Author
            };
        }
    }
}
