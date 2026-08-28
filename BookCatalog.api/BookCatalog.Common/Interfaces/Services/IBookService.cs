using BookCatalog.Common.Models.Dtos.Requests;
using BookCatalog.Common.Models.Dtos.Responses;

namespace BookCatalog.Common.Interfaces.Services
{
    public interface IBookService
    {
        PagedResult<BookResponse> GetAll(BookFilter filter);
        BookResponse? Get(int id);
        BookResponse Create(CreateBookRequest request);
        BookResponse? Update(UpdateBookRequest request);
        bool Delete(int id);
    }
}
