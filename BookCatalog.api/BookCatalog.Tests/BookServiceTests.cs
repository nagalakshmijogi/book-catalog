using BookCatalog.BusinessLogicLayer.Services;
using BookCatalog.Common.Interfaces.Repositories;
using BookCatalog.Common.Models.Dtos.Requests;
using BookCatalog.Common.Models.Dtos.Responses;
using Moq;

namespace BookCatalog.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public void Get_WhenBookExists_ReturnsBook()
        {
      
            var book = new BookResponse
            {
                Id = 1,
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 2021
            };

            var mockRepository = new Mock<IBookRepository>();

            mockRepository.Setup(x => x.Get(1)).Returns(book);

            var service = new BookService(mockRepository.Object);

            var result = service.Get(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test book", result.Title);
        }
        [Fact]
        public void Get_WhenBookDoesNotExist_ReturnsNull()
        {
           
            var mockRepository = new Mock<IBookRepository>();

            mockRepository.Setup(x => x.Get(99)).Returns((BookResponse)null!);

            var service = new BookService(mockRepository.Object);

            var result = service.Get(99);

            Assert.Null(result);
        }
        [Fact]
        public void Create_WhenRequestIsValid_ReturnsCreatedBook()
        {
           
            var request = new CreateBookRequest
            {
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 2021
            };

            var expectedBook = new BookResponse
            {
                Id = 1,
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 2021
            };

            var mockRepository = new Mock<IBookRepository>();

            mockRepository.Setup(x => x.Create(request)).Returns(expectedBook);

            var service = new BookService(mockRepository.Object);

            var result = service.Create(request);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test book", result.Title);
        }
        [Fact]
        public void Update_WhenBookExists_ReturnsUpdatedBook()
        {
          
            var request = new UpdateBookRequest
            {
                Id = 1,
                Title = "Test book Updated",
                Author = "Test author",
                PublishedYear = 2021
            };

            var expectedBook = new BookResponse
            {
                Id = 1,
                Title = "Test book Updated",
                Author = "Test author",
                PublishedYear = 2021
            };

            var mockRepository = new Mock<IBookRepository>();

            mockRepository.Setup(x => x.Update(request)).Returns(expectedBook);

            var service = new BookService(mockRepository.Object);

            var result = service.Update(request);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test book Updated", result.Title);
        }
        [Fact]
        public void Update_WhenBookDoesNotExist_ReturnsNull()
        {
           
            var request = new UpdateBookRequest
            {
                Id = 999,
                Title = "Test book Updated",
                Author = "Test author",
                PublishedYear = 2021
            };

            var mockRepository = new Mock<IBookRepository>();

            mockRepository.Setup(x => x.Update(request)).Returns((BookResponse)null!);

            var service = new BookService(mockRepository.Object);

            var result = service.Update(request);

            Assert.Null(result);
        }
        [Fact]
        public void Delete_WhenBookExists_ReturnsTrue()
        {
           var id = 1;

            var mockRepository = new Mock<IBookRepository>();

            mockRepository.Setup(x => x.Delete(id)).Returns(true);

            var service = new BookService(mockRepository.Object);

            var result = service.Delete(id);

            Assert.True(result);
        }
        [Fact]
        public void Delete_WhenBookDoesNotExist_ReturnsFalse()
        {
           var id = 999;

            var mockRepository = new Mock<IBookRepository>();

            mockRepository .Setup(x => x.Delete(id)).Returns(false);

            var service = new BookService(mockRepository.Object);

            var result = service.Delete(id);

            Assert.False(result);
        }
        [Fact]
        public void GetAll_ReturnsPagedResult()
        {
            
            var filter = new BookFilter
            {
                PageNumber = 2,
                PageSize = 2
            };

            var expectedResult = new PagedResult<BookResponse>
            {
                Items = new List<BookResponse>
        {
            new BookResponse
            {
                Id = 3,
                Title = "Book 3",
                Author = "Author 3",
                PublishedYear = 2020
            },
            new BookResponse
            {
                Id = 4,
                Title = "Book 4",
                Author = "Author 4",
                PublishedYear = 2021
            }
        },
                PageNumber = 2,
                PageSize = 2,
                TotalCount = 4,
                TotalPages = 2
            };

            var mockRepository = new Mock<IBookRepository>();

            mockRepository .Setup(x => x.GetAll(filter)).Returns(expectedResult);

            var service = new BookService(mockRepository.Object);

            var result = service.GetAll(filter);

            Assert.NotNull(result);
            Assert.Equal(2, result.PageNumber);
            Assert.Equal(2, result.PageSize);
            Assert.Equal(4, result.TotalCount);
            Assert.Equal(2, result.TotalPages);
            Assert.Equal(2, result.Items.Count());
        }
        [Fact]
        public void GetAll_WithTitleFilter_ReturnsFilteredBooks()
        {
            var filter = new BookFilter
            {
                Title = "Test",
                PageNumber = 1,
                PageSize = 10
            };

            var expectedResult = new PagedResult<BookResponse>
            {
                Items = new List<BookResponse>
        {
            new BookResponse
            {
                Id = 1,
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 2021
            }
        },
                PageNumber = 1,
                PageSize = 10,
                TotalCount = 1,
                TotalPages = 1
            };

            var mockRepository = new Mock<IBookRepository>();

            mockRepository
                .Setup(x => x.GetAll(filter))
                .Returns(expectedResult);

            var service = new BookService(mockRepository.Object);

             var result = service.GetAll(filter);

            Assert.NotNull(result);
            Assert.Single(result.Items);
            Assert.Equal("Test book", result.Items.First().Title);
        }
        [Fact]
        public void GetAll_WithFilterAndPagination_ReturnsCorrectPage()
        {
           var filter = new BookFilter
            {
                Title = "Test",
                PageNumber = 2,
                PageSize = 2
            };

            var expectedResult = new PagedResult<BookResponse>
            {
                Items = new List<BookResponse>
        {
            new BookResponse
            {
                Id = 3,
                Title = "Test book and the Prisoner of Azkaban",
                Author = "Test author",
                PublishedYear = 1999
            },
            new BookResponse
            {
                Id = 4,
                Title = "Test book and the Goblet of Fire",
                Author = "Test author",
                PublishedYear = 2000
            }
        },
                PageNumber = 2,
                PageSize = 2,
                TotalCount = 4,
                TotalPages = 2
            };

            var mockRepository = new Mock<IBookRepository>();
            mockRepository .Setup(x => x.GetAll(filter)) .Returns(expectedResult);
            var service = new BookService(mockRepository.Object);
            var result = service.GetAll(filter);
            Assert.NotNull(result);
            Assert.Equal(2, result.PageNumber);
            Assert.Equal(2, result.PageSize);
            Assert.Equal(4, result.TotalCount);
            Assert.Equal(2, result.TotalPages);
            Assert.Equal(2, result.Items.Count());
        }
    }
}
