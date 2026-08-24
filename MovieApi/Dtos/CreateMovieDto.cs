using System.ComponentModel.DataAnnotations;
namespace MovieApi.Dtos
{
    public class CreateMovieDto
    {
        [Required]
        [StringLength(100)]

        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Genre { get; set; }
        [Required]
        [StringLength(100)]
        public string Director { get; set; }
        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }
        [Range(1, 500)]
        public int DurationMinutes { get; set; }
        [Range (0.0, 10.0)]
        public double Rating { get; set; }

        public string? Description { get; set; }
        
    }
}
