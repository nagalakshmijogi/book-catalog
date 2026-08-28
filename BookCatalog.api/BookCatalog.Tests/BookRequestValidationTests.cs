

using System.ComponentModel.DataAnnotations;
using BookCatalog.Common.Models.Dtos.Requests;

namespace BookCatalog.Tests
{
    public class BookRequestValidationTests
    {
        [Fact]
        public void CreateBookRequest_WhenTitleIsEmpty_IsInvalid()
        {
          
            var request = new CreateBookRequest
            {
                Title = "",
                Author = "Test author",
                PublishedYear = 2021
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

       
            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);

         Assert.False(isValid);
        }
        [Fact]
        public void CreateBookRequest_WhenAuthorIsEmpty_IsInvalid()
        {
          
            var request = new CreateBookRequest
            {
                Title = "Test book",
                Author = "",
                PublishedYear = 2021
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

          
            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);

           
            Assert.False(isValid);
        }
        [Fact]
        public void CreateBookRequest_WhenPublishedYearIsBelowMinimum_IsInvalid()
        {
           
            var request = new CreateBookRequest
            {
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 999
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

        
            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);

            Assert.False(isValid);
        }
        [Fact]
        public void CreateBookRequest_WhenPublishedYearIsAboveMaximum_IsInvalid()
        {
           
            var request = new CreateBookRequest
            {
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 2027
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);

            Assert.False(isValid);
        }
        [Fact]
        public void CreateBookRequest_WhenPublishedYearIsValid_IsValid()
        {
          
            var request = new CreateBookRequest
            {
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 2020
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

           
            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);

           Assert.True(isValid);
        }
        [Fact]
        public void UpdateBookRequest_WhenIdIsZero_IsInvalid()
        {
            
            var request = new UpdateBookRequest
            {
                Id = 0,
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 2020
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

           
            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);

            
            Assert.False(isValid);
        }
        [Fact]
        public void UpdateBookRequest_WhenIdIsValid_IsValid()
        {
           
            var request = new UpdateBookRequest
            {
                Id = 1,
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 2020
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

           
            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);

            Assert.True(isValid);
        }
        [Fact]
        public void UpdateBookRequest_WhenTitleIsEmpty_IsInvalid()
        {
       
            var request = new UpdateBookRequest
            {
                Id = 1,
                Title = "",
                Author = "Test author",
                PublishedYear = 2020
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

           
            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);
            Assert.False(isValid);
        }
        [Fact]
        public void UpdateBookRequest_WhenAuthorIsEmpty_IsInvalid()
        {
           
            var request = new UpdateBookRequest
            {
                Id = 1,
                Title = "Test book",
                Author = "",
                PublishedYear = 2020
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();


            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);
            Assert.False(isValid);
        }
        [Fact]
        public void UpdateBookRequest_WhenPublishedYearIsBelowMinimum_IsInvalid()
        {
         
            var request = new UpdateBookRequest
            {
                Id = 1,
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 999
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

          
            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);

           Assert.False(isValid);
        }
        [Fact]
        public void UpdateBookRequest_WhenPublishedYearIsAboveMaximum_IsInvalid()
        {
         
            var request = new UpdateBookRequest
            {
                Id = 1,
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 2027
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

            
            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);
            Assert.False(isValid);
        }
        [Fact]
        public void UpdateBookRequest_WhenPublishedYearIsValid_IsValid()
        {
          
            var request = new UpdateBookRequest
            {
                Id = 1,
                Title = "Test book",
                Author = "Test author",
                PublishedYear = 2020
            };

            var context = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(
                request,
                context,
                validationResults,
                true);
            Assert.True(isValid);
        }
        [Fact]
        public void BookFilter_PageNumberZero_IsInvalid()
        {
           var filter = new BookFilter
            {
                PageNumber = 0,
                PageSize = 10
            };
            var context = new ValidationContext(filter);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(
                filter,
                context,
                results,
                true);
            Assert.False(isValid);
        }
        [Fact]
        public void BookFilter_PageSizeZero_IsInvalid()
        {
            var filter = new BookFilter
            {
                PageNumber = 1,
                PageSize = 0
            };
            var context = new ValidationContext(filter);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(
                filter,
                context,
                results,
                true);
            Assert.False(isValid);
        }
    }
}
