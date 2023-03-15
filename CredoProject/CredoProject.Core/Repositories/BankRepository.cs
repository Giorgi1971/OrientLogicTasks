﻿using System;
using CredoProject.Core.Db;
using CredoProject.Core.Repositories;
using CredoProject.Core.Db.Entity;
using Microsoft.EntityFrameworkCore;

namespace CredoProject.Core.Repositories
{
    public interface IBankRepository
    {
        Task<UserEntity> GetCustomerById(int id);
        Task<AccountEntity> GetAccountById(int id);
        Task<CardEntity> GetCardById(int id);
        Task AddCustomerToDbAsync(UserEntity customer);
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

        public async Task AddCustomerToDbAsync(UserEntity customer)
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

        public async Task<UserEntity> GetCustomerById(int id)
        {
            var customer = await _db.CustomerEntities.SingleOrDefaultAsync(x => x.Id == id);
            return customer ?? throw new Exception("Customer Not Found!");
        }

        public async Task<AccountEntity> GetAccountById(int id)
        {
            var account = await _db.AccountEntities.SingleOrDefaultAsync(x => x.AccountEntityId == id);
            return account ?? throw new Exception("Account Not Found!");
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

