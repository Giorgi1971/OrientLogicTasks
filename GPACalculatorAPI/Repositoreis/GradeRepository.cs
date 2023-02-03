﻿using System;
using GPACalculatorAPI.Db.Entity;
using GPACalculatorAPI.Models.Requests;
using System.Collections.Generic;
using GPACalculatorAPI.Db;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Azure.Core;

namespace GPACalculatorAPI.Repositoreis
{
    public interface IGradeRepository
    {
        Task<GradeEntity> CreateGradeAsync(int id, CreateGradeRequest request);
        Task SaveChangesAsync();
        Task<List<int>> GetStudentGrades(int id);
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
        public async Task<List<int>> GetStudentGrades(int id)
        {
            var scores = _db.Grades
                .Where(g => g.StudentId == id)
                .Select(u => u.Score)
                .ToList();
            return await Task.FromResult(scores);
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

        public async Task<double> GetStudentGPAAsync(int studentId)
        {
            var totalGrade = 0;
            double total = 0;
            var studentGrades = _db.Grades.Where(x => x.StudentId==studentId);

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
            var top10student = _db.Students
                .Select(x => new { Student = x.Id, AverageSubject = GetStudentGPAAsync(x.Id).ToString() })
                .OrderBy(x => x.AverageSubject)
                .Take(2);


            List<StudentEntity> Top10List = new List<StudentEntity>();

            foreach (var item in top10student)
            {
                Top10List.Add(_db.Students.FirstOrDefault(x => x.Id == item.Student));
            }

            return Top10List;


        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
