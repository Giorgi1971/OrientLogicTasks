using System;
using MovieDatabaseAPI.Repositories;
using MovieDatabaseAPI.Data.Entity;

namespace MovieDatabaseAPI.Service
{
    public interface ICalculate
    {
        Task<List<string>> OneMovieJanresString(int id);
        //Task<List<GenreIdName>> ShowMoviesByOneJanreObjects(int id);
    }


    public class Calculate : ICalculate
    {
        private readonly IMovieRepository _movieRepository;

        public Calculate(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        public Task<List<string>> OneMovieJanresString(int id)
        {
            var data = _movieRepository.OneMovieJanresString(id);
            return data;
        }

        //public Task<List<GenreIdName>> ShowMoviesByOneJanreObjects(int id)
        //{
        //    var data = _movieRepository.ShowMoviesByOneJanreObjects(id);
        //    return data;
        //}

    }
}

