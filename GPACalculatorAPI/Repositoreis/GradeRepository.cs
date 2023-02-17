using System;
using GPACalculatorAPI.Db.Entity;
using GPACalculatorAPI.Models.Requests;
using System.Collections.Generic;
using GPACalculatorAPI.Db;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace GPACalculatorAPI.Repositoreis
{
    public interface IGradeRepository
    {
        Task<GradeEntity> CreateGradeAsync(int id, CreateGradeRequest request);
        Task SaveChangesAsync();
        Task<List<GradeEntity>> GetStudentGrades(int id);
        Task<double> GetStudentGPAAsync(int studentId);
        Task<List<SubjectEntity>> GetTop3Subject();
        Task<List<SubjectEntity>> GetBottom3Subject();
        Task<List<StudentEntity>> GetTop10StudentByGPA();
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
        public async Task<List<GradeEntity>> GetStudentGrades(int id)
        {
            var scores = await _db.Grades
                .Where(g => g.StudentId == id)
                .Select(u => new GradeEntity {
                    Score = u.Score, SubjectId = u.SubjectId})
                .ToListAsync();
            return scores;
        }

        public async Task<List<SubjectEntity>> GetTop3Subject()
        {
            var top3 = _db.Grades
                .GroupBy(x => x.SubjectId)
                .Select(x => new { Subject = x.Key, AverageSubject = x.Average(y => y.Score) })
                .OrderByDescending(x => x.AverageSubject)
                .Take(3);
            List<SubjectEntity> Top3Subjects = new List<SubjectEntity>();

            foreach (var item in top3)
            {
                Top3Subjects.Add(_db.Subjects.FirstOrDefault(x => x.Id == item.Subject));
            }

            return Top3Subjects;
        }

        public async Task<List<SubjectEntity>> GetBottom3Subject()
        {
            var top3 = _db.Grades
                .GroupBy(x => x.SubjectId)
                .Select(x => new { Subject = x.Key, AverageSubject = x.Average(y => y.Score) })
                .OrderBy(x => x.AverageSubject)
                .Take(3);
            List<SubjectEntity> Top3Subjects = new List<SubjectEntity>();

            foreach (var item in top3)
            {
                Top3Subjects.Add(_db.Subjects.FirstOrDefault(x => x.Id == item.Subject));
            }

            return Top3Subjects;
        }

        public IQueryable<GradeEntity> getGradesByStudentId(int studentId)
        {
            return _db.Grades.Where(x => x.StudentId == studentId);
        }

        public async Task<double> GetStudentGPAAsync(int studentId)
        {
            var totalGrade = 0;
            double total = 0;
            var studentGrades = getGradesByStudentId(studentId);

            foreach(var grade in studentGrades)
            {
                var subject = await _db.Subjects.FindAsync(grade.SubjectId);
                //if (subject == null)
                //    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "this item does not exist"));

                if (grade.Score > 90 && grade.Score <= 100)
                {
                    total = 4 * subject.Credit;
                    totalGrade += subject.Credit;
                }
                if (grade.Score > 80 && grade.Score <= 90)
                {
                    total = 3 * subject.Credit;
                    totalGrade += subject.Credit;
                }
                if (grade.Score > 70 && grade.Score <= 80)
                {
                    total = 2 * subject.Credit;
                    totalGrade += subject.Credit;
                }
                if (grade.Score > 60 && grade.Score <= 70)
                {
                    total = 1 * subject.Credit;
                    totalGrade += subject.Credit;
                }
                if (grade.Score > 50 && grade.Score <= 60)
                {
                    total = 0.5 * subject.Credit;
                    totalGrade += subject.Credit;
                }
            }
            return total/totalGrade;
        }

        public async Task<List<StudentEntity>> GetTop10StudentByGPA()
        {
            //var top10student = _db.Students
            //    .Select(x => new { Student = x.Id, AverageSubject = GetStudentGPAAsync(x.Id) })
            //    .OrderBy(x => x.AverageSubject)
            //    .Take(2);

            //List<StudentEntity> Top10List = new List<StudentEntity>();

            //foreach (var item in top10student)
            //{
            //    Top10List.Add(_db.Students.FirstOrDefault(x => x.Id == item.Student));
            //}

            //return  Top10List;

            var allStudents = _db.Students;
            List<StudentEntity> top10StudentByGPA = new List<StudentEntity>();
            List<double> StudentsGPA = new List<double>();


            foreach (var student in allStudents)
            {
                StudentsGPA.Add(await GetStudentGPAAsync(student.Id));
            }

            var topTwoIndexes = StudentsGPA.Select((number, index) => new { number, index })
                                          .OrderBy(x => x.number)
                                          .Take(2)
                                          .Select(x => x.index)
                                          .ToList();

            foreach (var index in topTwoIndexes)
            {
                top10StudentByGPA.Add(_db.Students.FirstOrDefault(x => x.Id == index));
            }

            return top10StudentByGPA;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
