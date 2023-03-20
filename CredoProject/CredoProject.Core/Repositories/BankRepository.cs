using System;
using CredoProject.Core.Db;
using CredoProject.Core.Models.Responses;
using CredoProject.Core.Repositories;
using CredoProject.Core.Db.Entity;
using Microsoft.EntityFrameworkCore;

namespace CredoProject.Core.Repositories
{
    public interface IBankRepository
    {
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<AccountEntity> GetAccountById(int id);
        Task<AccountEntity> GetAccountByIBAN(string str);
        Task<CardEntity> GetCardById(int id);
        Task AddCustomerToDbAsync(UserEntity customer);
        Task AddAccountToDbAsync(AccountEntity account);
        Task AddCardToDbAsync(CardEntity card);
        Task<List<CustomerAccountsResponse>> GetUserAccountsFromDbAsync(int id);
        Task<List<CardEntity>> GetUserCardsFromDbAsync(int id);
        Task<List<string>> GetAccountsById(int id);
        Task SaveChangesAsync();
    }

    public class BankRepository : IBankRepository
    {
        private readonly CredoDbContext _db;

        public BankRepository(CredoDbContext db)
        {
            _db = db;
        }

        public async Task<List<string>> GetAccountsById(int id)
        {
            var result = await _db.AccountEntities
                .Where(x => x.CustomerEntityId == id)
                .Select(s => s.IBAN)
                .ToListAsync();
            return result;
        }

        public async Task<List<CustomerAccountsResponse>> GetUserAccountsFromDbAsync(int id)
        {
            var result = await _db.AccountEntities
                .Where(x => x.CustomerEntityId == id)
                .Include(sc => sc.FromTransactionEntities)
                .Select(y => new CustomerAccountsResponse { AccountEntityId = y.AccountEntityId, Amount = y.Amount, Currency = y.Currency, IBAN = y.IBAN, Transactionss = y.FromTransactionEntities })
                .ToListAsync();
            return result;
        }

        public async Task<List<CardEntity>> GetUserCardsFromDbAsync(int id)
        {
            var result = await _db.CardEntities
                .Where(x => x.AccountEntity.CustomerEntityId == id)
                .Include(x => x.AccountEntity)
                .ToListAsync();
            return result;
        }

        public async Task AddCustomerToDbAsync(UserEntity customer)
        {
            await _db.UserEntities.AddAsync(customer);
        }

        public async Task AddAccountToDbAsync(AccountEntity account)
        {
            await _db.AccountEntities.AddAsync(account);
        }

        public async Task AddCardToDbAsync(CardEntity card)
        {
            await _db.CardEntities.AddAsync(card);
        }

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            var customer = await _db.UserEntities.SingleOrDefaultAsync(x => x.Id == id);
            return customer ?? throw new Exception("Customer Not Found!");
        }

        public async Task<AccountEntity> GetAccountById(int id)
        {
            var account = await _db.AccountEntities.SingleOrDefaultAsync(x => x.AccountEntityId == id);
            return account ?? throw new Exception("Account Not Found!");
        }

        public async Task<AccountEntity> GetAccountByIBAN(string str)
        {
            var account = await _db.AccountEntities.SingleOrDefaultAsync(x => x.IBAN == str);
            return account ?? throw new Exception("Iban Not Found!");
        }

        public async Task<CardEntity> GetCardById(int id)
        {
            var card = await _db.CardEntities.SingleOrDefaultAsync(x => x.CardEntityId == id);
            return card ?? throw new Exception("Card Not Found!");
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

