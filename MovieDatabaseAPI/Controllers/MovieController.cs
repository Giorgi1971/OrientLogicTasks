using System;
using System.Collections.Generic;
using System.Linq;
using MovieDatabaseAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.Repositories;
using MovieDatabaseAPI.Data.Entity;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("SerchedMoviesWithPageIndex")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetSearchedMovies([FromQuery]GetSearchedMoviesRequest request)
        {
            try
            {
                return Ok(await _movieRepository.GetSearchedMovies(
                    request.Filter, request.pageSize, request.pageIndex
                    ));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("SerchedMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetSearchedMovies2([FromQuery]GetSearchedMoviesRequest2 request)
        {
            try
            {
                return Ok(await _movieRepository.GetSearchedMovies2(
                    request.FilterTitle, request.FilterDescription
                    ));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("AllMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            try
            {
                return Ok(await _movieRepository.AllMovie());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "ErrorrrrrrGetAll retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetEmployee(int id)
        {
            try
            {
                var result = await _movieRepository.GetMovie(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie([FromBody] CreateMovieRequest request)
        {
            // აქ if და ქვევით try-ის if ერთი და იგივეს ხომ არ აკეთებს? ან ეს საერთოდ ამისია???
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (request == null)
                    return BadRequest();
                // Add custom model validation error
                // აქამდე სავარაუდოდ [required] ატიბუტი არ მოუშვებს!!!
                var movie = request.Movie;
                if (movie == null)
                    return BadRequest("Movie info not in request body");
                if (movie.Title is null ||
                    movie.Description is null ||
                    movie.Description is null)
                {
                    return BadRequest("Title, Description and Movie Director is Required Value");
                }
                if (request.Movie.Releazed.Year < 1895)
                    return BadRequest("Movie Releazed date must after 1895 year");

                movie.MovieStatus = Status.active;
                movie.CreateAt = DateTime.Now;
                var createdMovie = await _movieRepository.AddMovie(movie);

                return CreatedAtAction(nameof(GetEmployee),
                    new { id = createdMovie.Id }, createdMovie);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error From Create 2 retrieving data from the database");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<Movie>> UpdateMovie([FromBody]UpdateMovieRequest request)
        {
            try
            {
                //  როგორ მივწვდე ურლ-ის id -ს??? if(id = request.Id)
                var movieToUpdate = await _movieRepository.GetMovie(request.Id);

                if (movieToUpdate == null)
                    return NotFound($"Employee with Id = {request.Id} not found");

                return await _movieRepository.UpdateMovie(request.Id, request.Title, request.Description, request.Director, request.Released);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // წინა ToDo -ში წაშლა არ მუშაობს, არც აქ მუშაობდა, სტატუსის შეცვლამ იმუშავა!
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Movie>> DeleteEmployee(int id)
        {
            try
            {
                var movieToDelete = await _movieRepository.GetMovie(id);

                if (movieToDelete == null)
                    return NotFound($"Movie with Id = {id} not found");

                _movieRepository.DeleteMovie(id);
                return Ok("Object Deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
