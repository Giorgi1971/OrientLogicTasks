using System;
using System.ComponentModel.DataAnnotations;
namespace MovieDatabaseAPI.Data.Entity
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Name must not be more than 100 characters.")]
        public string Description { get; set; }

        [Required]
        public DateTime Releazed { get; set; }

        [Required]
        public string MovieDirector { get; set; }

        [Required]
        public Status MovieStatus { get; set; }

        [Required]
        public DateTime CreateAt { get; set; }
    }

    public enum Status
    {
        active,
        deleted
    }
}
