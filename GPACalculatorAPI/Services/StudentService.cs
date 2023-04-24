using System;
using GPACalculatorAPI.Db.Entity;
using GPACalculatorAPI.Models.Requests;
using GPACalculatorAPI.Repositoreis;

namespace GPACalculatorAPI.Services
{
    public class StudentService
    {
        private readonly IStudentRepositor _studentRepositor;

        public StudentService(IStudentRepositor studentRepositor)
        {
            _studentRepositor = studentRepositor;
        }

        public async Task<decimal> GetStudentGPAAsync(int studentId)
        {
            var studentGrades = GetStudentGradesAsync(studentId).Result;
            var totalGrade = 0;
            decimal total = 0;
            decimal gp;
            foreach (var grade in studentGrades)
            {
                var subject = await _studentRepositor.GetSubject(grade.SubjectId);

                if (grade.Score > 90)
                {
                    gp = 4;
                }
                else if (grade.Score > 80)
                {
                    gp = 3;
                }
                else if (grade.Score > 70)
                {
                    gp = 2;
                }
                else if (grade.Score > 60)
                {
                    gp = 1;
                }
                else if (grade.Score > 50)
                {
                    gp = 0.5m;
                }
                else
                    continue;
                total += gp * subject.Credit;
                totalGrade += subject.Credit;
            }
            return total / totalGrade;
        }

        public async Task<List<GradeEntity>> GetStudentGradesAsync(int studentId)
        {
            var grades = await _studentRepositor.GetStudentGradesAsync(studentId);
            return grades;
        }

        public async Task<StudentEntity> CreateStudentAsync(CreateStudentRequest request)
        {
            validateRequest(request);
            StudentEntity student = CreateStudent(request);
            await _studentRepositor.AddStudentAsync(student);
            return student;
        }

        private static StudentEntity CreateStudent(CreateStudentRequest request)
        {
            var student = new StudentEntity()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Course = request.Course,
                PersonalNubmer = request.PersonalNubmer
            };
            return student;
        }

        public async Task<GradeEntity> CreateGradeAsync(int id, CreateGradeRequest request)
        {
            var newGrade = _studentRepositor.getGrade(id, request.SubjectId).Result;
            if (newGrade != null)
                newGrade.Score = request.Score;
            else
                newGrade = new GradeEntity() { StudentId = id, SubjectId = request.SubjectId, Score = request.Score };
            await _studentRepositor.AddGradeAsync(newGrade);
            return newGrade;
        }


        private void validateRequest(CreateStudentRequest request)
        {
            if (string.IsNullOrEmpty(request.FirstName))
                throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _studentRepositor.SaveChangesAsync();
        }

    }
}

