using System;
using System.ComponentModel.DataAnnotations;
using MovieDatabaseAPI.Data.Entity;
namespace MovieDatabaseAPI.Models
{
    // CreateMovieRequest იყენებს Movie Entity-ს და რა აზრი აქვს CreateMovieRequest ის შექმნას
    // ჯობია ცალკე ქონდეს თავისი აღწერა.
    // პლიუს კიდე Create -ის დროს არ უნდა გადმოგცეთ სხვამ Movie-ის Id ეს გასაკეთებელია თქვენით
    // CreateMovieRequests არ უნდა ქონდეს CreatedAt და Id ფროფერთი
    public class CreateMovieRequest
    {
        //private readonly CreateMovieRequest _createMovieRequest;

        //private readonly string _dateTimeNow;
        //private string _dateTimeNow
        //{
        //    get
        //    {
        //        return DateTime.Now.ToString("MM/dd/yyyy");
        //    }
        //}

        //public CreateMovieRequest()
        //{
        //    _createMovieRequest = new CreateMovieRequest();
        //}
            
        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Name must not be more than 200 characters or Lees 3!")]
        public string Title { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 3, ErrorMessage = "Name must not be more than 2000 characters or Lees 3!")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        // ასე როგორ გავაკეთო?? "1/1/2050" ასე მინდა ახლანდელი ჩანაწერი იყოს ცვლადით??
        //[Range(typeof(DateTime), "1/1/1887", _createMovieRequest._dateTimeNow(),  ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Range(typeof(DateTime), "1/1/1887", "1/1/2050", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime Releazed { get; set; }

        [Required]
        public string MovieDirector { get; set; }
    }
}

