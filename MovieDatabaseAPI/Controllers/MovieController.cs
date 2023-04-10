using System;
using System.Collections.Generic;
using System.Linq;
using MovieDatabaseAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.Repositories;
using MovieDatabaseAPI.Data.Entity;
using System.ComponentModel.DataAnnotations;
using MovieDatabaseAPI.Service;
using MovieDatabaseAPI.Data;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseAPI.ErrorSaver;
using static MovieDatabaseAPI.ErrorSaver.ErrorHandlerMiddleware;

namespace MovieDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController: ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ICalculate _service;
        private readonly AppDbContext _db;

        public MovieController(IMovieRepository movieRepository, ICalculate service, AppDbContext db)
        {
            _movieRepository = movieRepository;
            _service = service;
            _db = db;
        }


        // !!!!! თუ id-ს ინტზე დიდ ციფრს მივცემ, მიბუნებს სტატუსს 400-ს. აქ მიწერია 500 დააბრუნეო.
        [HttpGet("/{id}/Any")]
        public async Task<ActionResult> ListGanreByMovieId(int id)
        {
            throw new Exception("frfrfrf");
            try
            {
            var result = await _db.Genres
                //.Include(x => x.MovieGenres)
                .Where(x => x.MovieGenres.Any(mg => mg.MovieId == id))
                .ToListAsync();
            return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("es BadRequest requestisaa");
                throw new Exception("aq ratom ar Semodis??");
                //return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
            return Ok($"Movie {movieToDelete.Title} Deleted");
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
                request.Filter, request.pageSize, request.pageIndex));
        }


        [HttpGet("{id:int}/genres")]
        public async Task<ActionResult<Movie>> GetMovieWithGenresAsync(int id)
        {
            var result = await _movieRepository.GetMovieWithGenresAsync(id);
            if (result == null) return NotFound($"Movie with Id - {id} Not found, Message from Controller!");
            return result;
        }


        [HttpGet("genre/{id:int}/Genre's-all-movies")]
        public async Task<ActionResult<Genre>> GenresAsync(int id)
        {
            var result = await _movieRepository.GenresAsync(id);
            if (result == null) return NotFound($"Movie with Id - {id} Not found, Message from Controller!");
            return result;
        }


        [HttpGet("oneMovie-genres-strins-ByCalculate/{id:int}")]
        public async Task<ActionResult<List<string>>> OneMovieJanresString(int id)
        {
            var result = await _service.OneMovieJanresString(id);
            if (result == null) return NotFound($"Movie with Id - {id} Not found, Message from Controller!");
            return result;
        }


        [HttpGet("show-movies-byGenreId/{id:int}")]
        public async Task<ActionResult<List<Movie>>> ShowMoviesByOneJanreObjects(int id)
        {
            var result = await _movieRepository.ShowMoviesByOneJanreObjects(id);
            if (result == null) return NotFound($"Movie with Id - {id} Not found, Message from Controller!");
            return result;
        }
    }
}
