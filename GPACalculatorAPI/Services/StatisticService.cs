using System;
using GPACalculatorAPI.Repositoreis;

namespace GPACalculatorAPI.Services
{
    public class StatisticService
    {

        private readonly IGradeRepository _gradeRepositor;

        public StatisticService(IGradeRepository gradeRepositor)
        {
            _gradeRepositor = gradeRepositor;
        }

        public async Task<List<string>> GetTop3SubjectAsync()
        {
            var ll = _gradeRepositor.GetTop3SubjectAsync();
            return ll.Result;
        }


    }
}

