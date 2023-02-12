using System;

using System.ComponentModel.DataAnnotations;
namespace MovieDatabaseAPI.Data.Entity
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Name must in range from 3 upto 100 characters.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(2000, ErrorMessage = "Name must not be more than 100 characters.")]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Releazed { get; set; }

        [Required]
        public string MovieDirector { get; set; } = null!;

        public Status MovieStatus { get; set; }

        public DateTime CreateAt { get; set; }

        public List<MovieGenre> MovieGenres { get; set; } = null!;

    }

    public enum Status
    {
        active,
        deleted
    }
}
