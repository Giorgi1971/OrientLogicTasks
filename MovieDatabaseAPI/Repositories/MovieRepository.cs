using System;
using MovieDatabaseAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieDatabaseAPI.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace MovieDatabaseAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> AddMovie(Movie movie);
        Task<Movie> GetMovie(int movieId);
        // ქვედა ხაზზე Filter-ის ნაცვლად Object რატომ არ შემიძლია???
        Task<List<Movie>> GetSearchedMovies(Filter filter, int pageSize, int pageIndex);
        Task<Movie> UpdateMovie(Movie movie);
        void DeleteMovie(int movieId);
    }

    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _db;

        public MovieRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            var result = await _db.Movies.AddAsync(movie);
            // ამას აქ უწერია ჩაწერა. await რადგან აქვს, აქ ხომ არ ჯობს როგორცაა?
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Movie> GetMovie(int movieId)
        {
            return await _db.Movies.FirstOrDefaultAsync(e => e.Id == movieId);
        }

        // აქ ტასკის ლისტად გადაკეთება მომიწია :( ??? ქვედა მინდოდა რომ ყოფილიყო
        //public Task<IEnumerable<Movie>> GetSearchedMovies(Filter filter, int pageSize, int pageIndex)
        public async Task<List<Movie>> GetSearchedMovies(Filter filter, int pageSize, int pageIndex)
        {
            var searchedMovies = _db.Movies.
                Where(
                m => m.Title.Contains(filter.InTitle) ||
                m.Description.Contains(filter.InDescription) ||
                m.MovieDirector.Contains(filter.InMovieDirector) ||
                m.Releazed.ToString() == filter.InReleasedDate.ToString()
                )
                .Skip(pageIndex*pageSize)
                .Take(pageSize)
                .OrderBy(t => t.Title)
                // აქ ასინქ რატომ შეიძლება???
                .ToListAsync();

            return await searchedMovies;
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            var result = await _db.Movies
                .FirstOrDefaultAsync(e => e.Id == movie.Id);

            if (result != null)
            {
                result.Title = movie.Title;
                result.Description = movie.Description;
                result.Releazed = movie.Releazed;
                result.MovieDirector = movie.MovieDirector;
                result.MovieStatus = movie.MovieStatus;
                result.CreateAt = movie.CreateAt;

                await _db.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async void DeleteMovie(int movieId)
        {
            var result = await _db.Movies
                .FirstOrDefaultAsync(m => m.Id == movieId);
            if (result != null)
            {
                _db.Movies.Remove(result);
                await _db.SaveChangesAsync();
            }
        }
    }

    public class Filter
    {
        public string InTitle { get; set; }
        public string InDescription { get; set; }
        public string InMovieDirector { get; set; }
        public int InReleasedDate { get; set; }
    }
}
