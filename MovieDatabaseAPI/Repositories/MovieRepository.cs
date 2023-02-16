using System;
using MovieDatabaseAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieDatabaseAPI.Data.Entity;
using MovieDatabaseAPI.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc;
using MovieDatabaseAPI.ErrorSaver;

namespace MovieDatabaseAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMoviesAsync();
        Task<List<Movie>> SearchMoviesWithPageIndexAsync(FilterMovie filter, int pageSize, int pageIndex);
        Task<Movie> AddMovieAsync(CreateMovieRequest request);
        Task<Movie> GetMovieAsync(int movieId);
        Task<List<string>> OneMovieJanresString(int id);
        Task<List<Movie>> ShowMoviesByOneJanreObjects(int id);
        Task<Movie> GetMovieWithGenresAsync(int movieId);
        Task<ActionResult<Genre>> GenresAsync(int id);
        Task<Movie> UpdateMovieAsync(int id, string title, string desc, string dir, DateTime date);
        Task DeleteMovie(int movieId);
        Task SaveChangesAsync();
    }

    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _db;

        public MovieRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Movie>> ShowMoviesByOneJanreObjects(int id)
        {
            var result = await _db.Movies
                //.Include(x => x.MovieGenres)
                //.ThenInclude(m => m.Movie)
                .Where(d => d.MovieGenres.Any(c => c.GenreId == id) && d.MovieStatus == Status.active)
                .ToListAsync();
            return result;
        }


        public async Task<List<string>> OneMovieJanresString(int id)
        {
            var oneMovieGenres = await _db.Movies
                .Where(m => m.Id == id && m.MovieStatus == Status.active)
                .Join(_db.MovieGenres, m => m.Id, mg => mg.MovieId, (m, mg) => mg)
                .Join(_db.Genres, mg => mg.GenreId, g => g.Id, (mg, g) => g)
                .Select(g => g.GenreName)
                .OrderBy(g => g)
                .ToListAsync();
            return oneMovieGenres;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            try
            {
            var allMovies = await _db.Movies
                .Where(m => m.MovieStatus == Status.active)
                .ToListAsync();
            return allMovies;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Movie>> SearchMoviesWithPageIndexAsync(FilterMovie filter, int pageSize, int pageIndex)
        {
            List<Movie> searchedMovies;
            try
            {
                if (pageSize == 0)
                    throw new Exception("pageSize Must not be 0");
                searchedMovies = await _db.Movies.
                    Where(
                    m => m.Title.Contains(filter.InTitle) ||
                    m.Description.Contains(filter.InDescription) ||
                    m.MovieDirector.Contains(filter.InMovieDirector) ||
                    m.Releazed.Year == filter.InReleasedDate)
                    .Where(m => m.MovieStatus == 0)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .OrderBy(t => t.Title)
                    .ToListAsync();
            }
            catch (Exception)
            {
                searchedMovies = new List<Movie>();
            }
            return searchedMovies;
        }

        public async Task<Movie> AddMovieAsync(CreateMovieRequest request)
        {
            var movie = new Movie()
            {
                Title = request.Title,
                Description = request.Description,
                Releazed = request.Releazed,
                MovieDirector = request.MovieDirector,
                CreateAt = DateTime.Now,
                MovieStatus = Status.active
            };
            var result = await _db.Movies.AddAsync(movie);
            return result.Entity;
        }


        public async Task<Movie> GetMovieAsync(int movieId)
        {
            var result = await _db.Movies.FirstOrDefaultAsync(
                e => e.Id == movieId && e.MovieStatus != Status.deleted
                );
            if (result == null) throw new Exception($"No Movie with this {movieId} Id (throw in repository)");
            return result;
        }


        public async Task<Movie> GetMovieWithGenresAsync(int movieId)
        {
            var result = await _db.Movies
                .Include(x => x.MovieGenres)
                .ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync(
                t => t.Id == movieId && t.MovieStatus == Status.active
                );
            return result;
        }


        public async Task<ActionResult<Genre?>> GenresAsync(int id)
        {
            try
            {
                var result = await _db.Genres
                    .Include(x => x.MovieGenres)
                    .ThenInclude(m => m.Movie)
                    .FirstOrDefaultAsync(
                    t => t.Id == id && t.MovieGenres.Any(d => d.Movie.MovieStatus == Status.active)
                    );
                return result;
            }
            catch (Exception ex)
            {
                ErrorLogHelper2.LogError(ex, _db);
                return null;
            }
        }


        public async Task<Movie> UpdateMovieAsync(int movieId, string title, string desc, string dir, DateTime date)
        {
            var result = await _db.Movies
                .FirstOrDefaultAsync(e => e.Id == movieId && e.MovieStatus != Status.deleted);

            date = new DateTime();
            if (result == null)
                throw new Exception("No movie found to update (Id = {movieId})! (throw in repository)");
            {
                if (!string.IsNullOrEmpty(title)) { result.Title = title; }
                if (!string.IsNullOrEmpty(desc)) { result.Description = desc; }
                if (!string.IsNullOrEmpty(dir)) { result.MovieDirector = dir; }
                if (date != DateTime.MinValue) { result.Releazed = date; }
                _db.Movies.Update(result);
                return result;
            }
        }

        // ეს ტასკი რას აბრუნებს (Ok არ აბრუნებს)
        public Task DeleteMovie(int movieId)
        {
            var result = _db.Movies
                    .FirstOrDefault(e => e.Id == movieId && e.MovieStatus != Status.deleted);

            if (result != null)
            {
                result.MovieStatus = Status.deleted;
            }
            return Task.CompletedTask;
        }


        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
