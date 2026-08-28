using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Common.Models.Dtos.Requests
{
    public class UpdateBookRequest
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Author { get; set; } = string.Empty;
        [Range(1000, 2026)]
        public int PublishedYear { get; set; }
    }
}
