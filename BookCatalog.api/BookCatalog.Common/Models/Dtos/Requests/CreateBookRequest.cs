using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookCatalog.Common.Models.Dtos.Requests
{
    public class CreateBookRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        [Range(1000, 2026)]
        public int PublishedYear { get; set; }
    }
}
