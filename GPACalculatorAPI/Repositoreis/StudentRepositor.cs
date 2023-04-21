using System;
using GPACalculatorAPI.Db.Entity;
using GPACalculatorAPI.Models.Requests;
using System.Collections.Generic;
using GPACalculatorAPI.Db;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace GPACalculatorAPI.Repositoreis
{
    public interface IStudentRepositor
    {
        Task<List<GradeEntity>> GetStudentGradesAsync(int studentId);
        Task<GradeEntity> getGrade(int studentId, int subjectId);
        Task AddStudentAsync(StudentEntity student);
        Task AddGradeAsync(GradeEntity grade);
        Task<SubjectEntity> GetSubject(int subjectId);
        Task SaveChangesAsync();
    }

    public class StudentRepositor :IStudentRepositor
    {
        private readonly AppDbContext _db;

        public StudentRepositor(AppDbContext db)
        {
            _db = db;
        }

        public async Task<SubjectEntity> GetSubject(int subjectId)
        {
            return await _db.Subjects.FirstAsync(x => x.Id == subjectId);
        }

        public async Task<List<GradeEntity>> GetStudentGradesAsync(int studentId)
        {
            return await _db.Grades.Where(x => x.StudentId == studentId).ToListAsync();
        }

        public async Task<GradeEntity> getGrade(int studentId, int subjectId)
        {
            return await _db.Grades.SingleOrDefaultAsync(x => x.StudentId == studentId && x.SubjectId == subjectId);
        }

        public async Task AddStudentAsync(StudentEntity student)
        {
            await _db.Students.AddAsync(student);
        }

        public async Task AddGradeAsync(GradeEntity grade)
        {
            await _db.Grades.AddAsync(grade);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }


    }
}

