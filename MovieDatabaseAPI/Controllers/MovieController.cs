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

        [HttpGet("AllMovies")]
        public async Task<ActionResult> GetMoviesAsync()
        {
            try
            {
                return Ok(await _movieRepository.GetMoviesAsync());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "ErrorrrrrrGetAll retrieving data from the database");
            }
        }

        [HttpGet("SerchedMoviesWithPageIndex")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetSearchedMovies([FromQuery]GetSearchedMoviesRequest request)
        {
            try
            {
                return Ok(await _movieRepository.GetSearchedMoviesAsync(
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
        public async Task<ActionResult<IEnumerable<Movie>>> GetSearchedMovies2Async([FromQuery]GetSearchedMoviesRequest2 request)
        {
            try
            {
                return Ok(await _movieRepository.GetSearchedMovies2Async(
                    request.FilterTitle, request.FilterDescription
                    ));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetMovieAsync(int id)
        {
            try
            {
                var result = await _movieRepository.GetMovieAsync(id);

                if (result == null) return NotFound();

                return result;
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Movie>> AddMovieAsync([FromBody]CreateMovieRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdMovie = await _movieRepository.AddMovieAsync(request);
                await _movieRepository.SaveChangesAsync();
                // ამას რა ჯანდაბა უნდა, ნახევარი დღე მომაცდინა 
                //return CreatedAtAction(nameof(GetMovieAsync),
                //    new { id = createdMovie.Id }, createdMovie);
                return Ok(createdMovie);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error From Create 2 retrieving data from the database");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<Movie>> UpdateMovieAsync([FromBody]UpdateMovieRequest request)
        {
            try
            {
                //  როგორ მივწვდე ურლ-ის id -ს??? if(id = request.Id)
                var movieToUpdate = await _movieRepository.GetMovieAsync(request.Id);

                if (movieToUpdate == null)
                    return NotFound($"Movie with Id = {request.Id} not found");
                var updatedMovie = await _movieRepository.UpdateMovieAsync(request.Id, request.Title, request.Description, request.Director, request.Released);
                await _movieRepository.SaveChangesAsync();

                return updatedMovie;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // წინა ToDo -ში წაშლა არ მუშაობს, არც აქ მუშაობდა, სტატუსის შეცვლამ იმუშავა!
        // 
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<Movie>> DeleteMovieAsync(int id)
        {
            try
            {
                var movieToDelete = await _movieRepository.GetMovieAsync(id);

                if (movieToDelete == null)
                    return NotFound($"Movie with Id = {id} not found");

                _movieRepository.DeleteMovie(id);
                await _movieRepository.SaveChangesAsync();
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
