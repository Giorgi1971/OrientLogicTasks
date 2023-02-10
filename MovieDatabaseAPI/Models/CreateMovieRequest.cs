using System;
using System.ComponentModel.DataAnnotations;
using MovieDatabaseAPI.Data.Entity;
namespace MovieDatabaseAPI.Models
{
    public class CreateMovieRequest
    {
        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "The name could not be more than 200  or less 2 characters!")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(2000, MinimumLength = 3, ErrorMessage = "The name could not be more than 2000  or less 3 characters!")]
        public string Description { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        // ასე როგორ გავაკეთო?? "1/1/2050" ასე მინდა ახლანდელი ჩანაწერი იყოს ცვლადით??
        //[Range(typeof(DateTime), "1/1/1887", _createMovieRequest._dateTimeNow(),  ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Range(typeof(DateTime), "1/1/1887", "1/1/2050", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime Releazed { get; set; }

        [Required]
        public string MovieDirector { get; set; } = null!;
    }
}
