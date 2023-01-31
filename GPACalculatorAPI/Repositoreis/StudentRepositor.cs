using System;
using GPACalculatorAPI.Db.Entity;
using GPACalculatorAPI.Models.Requests;
using System.Collections.Generic;
using GPACalculatorAPI.Db;

namespace GPACalculatorAPI.Repositoreis
{
    public interface IStudentRepositor
    {
        Task<StudentEntity> CreateStudenAsync(CreateStudentRequest request);
        Task SaveChangesAsync();
    }

    public class StudentRepositor :IStudentRepositor
    {
        private readonly AppDbContext _db;

        public StudentRepositor(AppDbContext db)
        {
            _db = db;
        }

        public async Task<StudentEntity> CreateStudenAsync(CreateStudentRequest request)
        {
            var student = new StudentEntity();
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Course = request.Course;
            student.PersonalNubmer = request.PersonalNubmer;
            await _db.Students.AddAsync(student);

            return student;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}

