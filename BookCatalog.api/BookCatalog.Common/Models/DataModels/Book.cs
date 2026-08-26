using System;
using System.Collections.Generic;
using System.Text;

namespace BookCatalog.Common.Models.DataModels
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
    }
}
