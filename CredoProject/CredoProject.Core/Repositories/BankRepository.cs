using System;
using CredoProject.Core.Db;
using CredoProject.Core.Repositories;
using CredoProject.Core.Db.Entity;
using Microsoft.EntityFrameworkCore;

namespace CredoProject.Core.Repositories
{
    public interface IBankRepository
    {
        Task<CustomerEntity> GetCustomerById(int id);
        Task AddCustomerToDbAsync(CustomerEntity customer);
        Task AddAccountToDbAsync(AccountEntity account);
        Task AddCardToDbAsync(CardEntity card);

        Task SaveChangesAsync();
    }


    public class BankRepository : IBankRepository
    {
        private readonly CredoDbContext _db;

        public BankRepository(CredoDbContext db)
        {
            _db = db;
        }

        public async Task AddCustomerToDbAsync(CustomerEntity customer)
        {
            await _db.CustomerEntities.AddAsync(customer);
        }

        public async Task AddAccountToDbAsync(AccountEntity account)
        {
            await _db.AccountEntities.AddAsync(account);
        }

        public async Task AddCardToDbAsync(CardEntity card)
        {
            await _db.CardEntities.AddAsync(card);
        }

        public async Task<CustomerEntity> GetCustomerById(int id)
        {
            var customer = await _db.CustomerEntities.FirstOrDefaultAsync(x => x.Id == id);
            return customer ?? throw new Exception("Now new Exeption");
        }


        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

