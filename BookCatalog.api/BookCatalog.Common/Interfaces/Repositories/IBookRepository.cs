using BookCatalog.Common.Models.Dtos.Requests;
using BookCatalog.Common.Models.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookCatalog.Common.Interfaces.Repositories
{
    public interface IBookRepository
    {
        List<BookResponse> GetAll();
        BookResponse Get(int id);
        BookResponse Create(CreateBookRequest request);
        BookResponse Update(UpdateBookRequest request);
        bool Delete(int id);

    }
}
