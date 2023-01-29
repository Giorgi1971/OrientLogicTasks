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
        Task<List<Movie>> GetMoviesAsync();
        //როცა async ვიყენებთ მეთოდში ჯობია მეთოდის სახელს ბოლოში დავუმატოტ Async
        //მაგალითად GetSearchedMoviesAsync(); AddMovieAsync();
        Task<List<Movie>> GetSearchedMovies(FilterMovie filter, int pageSize, int pageIndex);
        Task<List<Movie>> GetSearchedMovies2(string filter1, string filter2);
        Task<Movie> AddMovie(Movie movie);
        //GetMovieById უკეთესი სახელი იქნებოდა უფრო meaningfull
        Task<Movie> GetMovie(int movieId);
        // ქვედა ხაზზე Filter-ის ნაცვლად Object რატომ არ შემიძლია???
        Task<Movie> UpdateMovie(int id, string title, string desc, string dir, DateTime date);
        //DeleteMovie ასინქრონული უნდა იყოს ამიტომ Task DeleteMovieAsync უნდა ერქვას
        // და ზოგადად _db.SaveChangesAsync() ჯობია Controller ში გამოიძახოთ
        // იმიტორო შეიძლება 1ს კი არა 3 რაღაცას ცვლით და ყოველ შეცვლაზე SaveChangesAsync-ის გამოყენებას
        // ერთხელ ბოლოში დაუწეროთ ჯობია
        void DeleteMovie(int movieId);
        // saveChanges მეტოდი აკლია აშკარად.... კი უნდა დაუწეროთ Task SaveChangesAsync();
    }

    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _db;

        public MovieRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            var allMovies = _db.Movies
                .Where(m => m.MovieStatus == Status.active)
                .ToListAsync();
            return await allMovies;
                //.Where (m => m.MovieStatus == 0)
        }


        // აქ ტასკის ლისტად გადაკეთება მომიწია :( ??? ქვედა მინდოდა რომ ყოფილიყო
        //public Task<IEnumerable<Movie>> GetSearchedMovies(Filter filter, int pageSize, int pageIndex)
        // იმუსაებს Task<IEnumerable<Movie>> რო დაუწეროთ, interface ში შეცვალეთ მეთოდი რო მაგას აბრუნებდეს
        // და იმუშავებს ჩვეულებრივად 
        public async Task<List<Movie>> GetSearchedMovies(FilterMovie filter, int pageSize, int pageIndex)
        {

            var searchedMovies = _db.Movies.
                Where(
                m => m.Title.Contains(filter.InTitle) ||
                m.Description.Contains(filter.InDescription) ||
                m.MovieDirector.Contains(filter.InMovieDirector) ||
                m.Releazed.Year == filter.InReleasedDate
                )
                .Where(m => m.MovieStatus == 0)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .OrderBy(t => t.Title)
                // აქ ასინქ რატომ შეიძლება???
                .ToListAsync();

            return await searchedMovies;
        }

        public async Task<List<Movie>> GetSearchedMovies2(string filterTitle, string filterDesc)
        {
            var searchedMovies = _db.Movies
                .Where(m => m.MovieStatus == 0)
                .Where(
                m => m.Title.Contains(filterTitle) ||
                m.Description.Contains(filterDesc)
                //m.Releazed.ToString() == filter.InReleasedDate.ToString()
                )
                .OrderBy(t => t.Title)
                .ToListAsync();

            return await searchedMovies;
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            var result = await _db.Movies.AddAsync(movie);
            // ამას აქ უწერია ჩაწერა. await რადგან აქვს, აქ ხომ არ ჯობს როგორცაა?
            // არა ცალკე ფუნქცია გააკეთეთ SaveChangesAsync() და controllerში გამოიძახეთ
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Movie> GetMovie(int movieId)
        {
            return await _db.Movies.FirstOrDefaultAsync(
                e => e.Id == movieId && e.MovieStatus != Status.deleted
                );
        }

        public async Task<Movie> UpdateMovie(int movieId, string title, string desc, string dir, DateTime date)
        {
            var result = await _db.Movies
                .FirstOrDefaultAsync(e => e.Id == movieId && e.MovieStatus != Status.deleted);

            if (result != null)
            {
                result.Title = title;
                result.Description = desc;
                result.Releazed = date;
                result.MovieDirector = dir;
                // ამ ხაზსაც ხომ არ ჭირდება განახლება:  არა იყოს როგორც არის CreatedAt არ იცვლება
                //result.CreateAt = DateTime.UtcNow;

                await _db.SaveChangesAsync();
                return result;
            }
            return null;
        }

        // რატომღაც (დაახლოებით, ბაზას აკითხავდა დამთავრებამდე ხელახლა)
        // ასინქრონულობის მოცილებამ უშველა????
        // არა ასინქრონული უნდა იყოს ზოგადად როცა ბაზასთან გვაქ კომუნიკაცია 
        // მიღებული პრაქტიკაა რომ Async/Await გამოვიყენოთ
        public void DeleteMovie(int movieId)
        {
            var result2 = _db.Movies
                    .FirstOrDefault(e => e.Id == movieId && e.MovieStatus != Status.deleted);

            if (result2 != null)
            {
                result2.MovieStatus = Status.deleted;
                _db.SaveChanges();
            }
        }
    }

    public class FilterMovie
    {
        public string InTitle { get; set; }
        public string InDescription { get; set; }
        public string InMovieDirector { get; set; }
        public int InReleasedDate { get; set; }
    }
}
