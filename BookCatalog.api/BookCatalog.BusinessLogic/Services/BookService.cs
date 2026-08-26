using BookCatalog.Common.Interfaces.Repositories;
using BookCatalog.Common.Interfaces.Services;
using BookCatalog.Common.Models.Dtos.Requests;
using BookCatalog.Common.Models.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookCatalog.BusinessLogicLayer.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public BookResponse Create(CreateBookRequest request)
        {
            return _bookRepository.Create(request);
        }

        public bool Delete(int id)
        {
            return _bookRepository.Delete(id);
        }

        public BookResponse Get(int id)
        {
            return _bookRepository.Get(id);
        }

        public List<BookResponse> GetAll()
        {
            return _bookRepository.GetAll();
        }

        public BookResponse Update(UpdateBookRequest request)
        {
            return _bookRepository.Update(request);
        }
    }
}
