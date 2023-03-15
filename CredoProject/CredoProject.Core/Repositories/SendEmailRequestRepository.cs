using System;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using Microsoft.EntityFrameworkCore;

namespace CredoProject.Core.Repositories
{
    public interface ISendEmailRequestRepository
    {
        void Insert(SendEmailRequestEntity entity);
        Task SaveChangesAsync();
    }

    public class SendEmailRequestRepository: ISendEmailRequestRepository
    {
        private readonly CredoDbContext _db;

        public SendEmailRequestRepository(CredoDbContext db)
        {
            _db = db;
        }

        public void Insert(SendEmailRequestEntity entity)
        {
            _db.SendEmailRequestEntities.Add(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }

}

