using System;
using GPACalculatorAPI.Db.Entity;
using GPACalculatorAPI.Models.Requests;
using System.Collections.Generic;
using GPACalculatorAPI.Db;

namespace GPACalculatorAPI.Repositoreis
{
    public interface IGradeRepository
    {
        Task<GradeEntity> CreateGradeAsync(int id, CreateGradeRequest request);
        Task SaveChangesAsync();
        Task<List<int>> GetStudentGrades(int id);
    }

    public class GradeRepository :IGradeRepository
    {
        private readonly AppDbContext _db;

        public GradeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<GradeEntity> CreateGradeAsync(int id, CreateGradeRequest request)
        {
            var grade = new GradeEntity();
            grade.StudentId = id;
            grade.SubjectId = request.SubjectId;
            grade.Score = request.Score;
            await _db.Grades.AddAsync(grade);

            return grade;
        }

        // აქ ასინქრონულობას ითხოვს და ავეითი ვერსად ჩავტენე, თუ ასინქს მოვუხსნი აწითლებს???
        public async Task<List<int>> GetStudentGrades(int id)
        {
            var scores = _db.Grades
                .Where(g => g.StudentId == id)
                .Select(u => u.Score)
                .ToList();
            return scores;
        }


        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }


    }
}

