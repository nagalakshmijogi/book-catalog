using BookCatalog.Common.Interfaces.Repositories;
using BookCatalog.Common.Models.DataModels;
using BookCatalog.Common.Models.Dtos.Requests;
using BookCatalog.Common.Models.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Text;

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

        public BookResponse Get(int id)
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

        public List<BookResponse> GetAll()
        {
            return _books.Select(book => new BookResponse()
            {
                Id = book.Id,
                Author = book.Author,
                PublishedYear = book.PublishedYear,
                Title = book.Title,
            }).ToList();
        }

        public BookResponse Update(UpdateBookRequest request)
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
