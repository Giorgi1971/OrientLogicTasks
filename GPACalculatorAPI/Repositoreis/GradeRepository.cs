using System;
using GPACalculatorAPI.Db.Entity;
using GPACalculatorAPI.Models.Requests;
using System.Collections.Generic;
using GPACalculatorAPI.Db;

namespace GPACalculatorAPI.Repositoreis
{
    public interface IGradeRepository
    {
        Task<GradeEntity> CreateGradeAsync(CreateGradeRequest request);
        Task SaveChangesAsync();
    }

    public class GradeRepository :IGradeRepository
    {
        private readonly AppDbContext _db;

        public GradeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<GradeEntity> CreateGradeAsync(CreateGradeRequest request)
        {
            var grade = new GradeEntity();
            grade.StudentId = request.StudentId;
            grade.SubjectId = request.SubjectId;
            grade.Score = request.Score;
            await _db.Grades.AddAsync(grade);

            return grade;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

