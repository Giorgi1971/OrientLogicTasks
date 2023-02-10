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
    public class MovieController: ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        [HttpGet("AllMovies")]
        public async Task<ActionResult> GetMoviesAsync()
        {
            return Ok(await _movieRepository.GetMoviesAsync());
        }


        [HttpPost("Create")]
        public async Task<ActionResult<Movie>> AddMovieAsync([FromBody] CreateMovieRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdMovie = await _movieRepository.AddMovieAsync(request);
            await _movieRepository.SaveChangesAsync();
            return Ok(createdMovie);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetMovieAsync(int id)
        {
            var result = await _movieRepository.GetMovieAsync(id);
            if (result == null) return NotFound($"Movie with Id - {id} Not found, Message from Controller!");
            return result;
        }


        [HttpDelete("{id:int}/delete")]
        public async Task<ActionResult<Movie>> DeleteMovieAsync(int id)
        {
            var movieToDelete = await _movieRepository.GetMovieAsync(id);

            if (movieToDelete == null)
                return NotFound($"Movie with Id = {id} not found");

            await _movieRepository.DeleteMovie(id);
            await _movieRepository.SaveChangesAsync();
            return Ok("Movie Deleted");
        }

        [HttpPut("{id:int}/update")]
        public async Task<ActionResult<Movie>> UpdateMovieAsync([FromBody] UpdateMovieRequest request)
        {
            var movieToUpdate = await _movieRepository.GetMovieAsync(request.Id);

            if (movieToUpdate == null)
                return NotFound($"Movie with Id = {request.Id} not found");
            var updatedMovie = await _movieRepository.UpdateMovieAsync(request.Id, request.Title, request.Description, request.Director, request.Released);
            await _movieRepository.SaveChangesAsync();

            return updatedMovie;
        }


        [HttpGet("search-movies-with-pageIndex")]
        public async Task<ActionResult<IEnumerable<Movie>>> SearchMoviesWithPageIndexAsync([FromQuery]GetSearchedMoviesRequest request)
        {
            return Ok(await _movieRepository.SearchMoviesWithPageIndexAsync(
                request.Filter, request.pageSize, request.pageIndex
                ));
        }

        [HttpGet("{id:int}/genres")]
        public async Task<ActionResult<Movie>> GetMovieWithGenresAsync(int id)
        {
            var result = await _movieRepository.GetMovieWithGenresAsync(id);
            if (result == null) return NotFound($"Movie with Id - {id} Not found, Message from Controller!");
            return result;
        }

        [HttpGet("genre/{id:int}")]
        public async Task<ActionResult<Genre>> GenresAsync(int id)
        {
            var result = await _movieRepository.GenresAsync(id);
            if (result == null) return NotFound($"Movie with Id - {id} Not found, Message from Controller!");
            return result;
        }



    }
}
