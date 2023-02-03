using System;
using GPACalculatorAPI.Db;
using GPACalculatorAPI.Db.Entity;
using GPACalculatorAPI.Models.Requests;

namespace GPACalculatorAPI.Repositoreis
{

    public interface ISubjectRepository
    {
        Task<SubjectEntity> CreateSubjectAsync(CreateSubjectRequest request);
        Task SaveChangesAsync();

    }

    public class SubjectRepository :ISubjectRepository
    {
        private readonly AppDbContext _db;

        public SubjectRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<SubjectEntity> CreateSubjectAsync(CreateSubjectRequest request)
        {
            var subject = new SubjectEntity();
            subject.Name = request.Name;
            subject.Credit = (int)request.Credit;
            await _db.Subjects.AddAsync(subject);

            return subject;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

