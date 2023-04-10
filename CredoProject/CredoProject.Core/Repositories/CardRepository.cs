using System;
using System.Diagnostics;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Models.Requests.Card;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CredoProject.Core.Repositories
{

    public interface ICardRepository
    {
        Task AddCardToDbAsync(CardEntity card);
        Task<CardEntity?> GetCardByNumberAndPinAsync(string cardNumber, string pinCode);
        Task<bool> UpdateCardPinAsync(string cardNumber, string newPinCode);
        Task SaveChangesAsync();
        Task<decimal> CalculateRate(Currency CurrencyFrom, Currency CurrencyTo);
        Task<ExchangeEntity> GetExchangeAsync(Currency CurrencyFrom, Currency CurrencyTo);
        Task AddTransactionInDb(TransactionEntity tran1);
        Task<decimal> withdraw24Db(CardEntity card);
    }

    public class CardRepository : ICardRepository
    {

        private readonly CredoDbContext _db;

        public CardRepository(CredoDbContext db)
        {
            _db = db;
        }

        public async Task<decimal> withdraw24Db(CardEntity card)
        {
            decimal sum = await _db.TransactionEntities
                .Where(x => x.CardId == card.CardEntityId && x.CreatedAt >= DateTime.Now.AddDays(-1))
                .SumAsync(x => x.AmountTransaction);

            return sum;
        }

        //public async Task<decimal> withdraw24Db(CardEntity card)
        //{
        //    decimal sum = await _db.TransactionEntities
        //        .Join(_db.CardEntities, t => t.AccountFromId, c => c.CardEntityId, (t, c) => new { Transaction = t, Card = c })
        //        .Where(tc => tc.Card.CardEntityId == card.CardEntityId && tc.Transaction.CreatedAt >= DateTime.Now.AddDays(-1))
        //        .SumAsync(tc => tc.Transaction.AmountTransaction);
        //    return sum;
        //}

        public async Task AddTransactionInDb(TransactionEntity tran1)
        {
            await _db.TransactionEntities.AddAsync(tran1);
        }

        public async Task<decimal> CalculateRate(Currency CurrencyFrom, Currency CurrencyTo)
        {
            var rate = await _db.exchangeEntities
                .SingleOrDefaultAsync(x => x.currencyFrom == CurrencyFrom && x.currencyTo == CurrencyTo);
            if (rate == null) throw new Exception("Not Found rate fom these currences!");
            return rate.rate;              
        }


        public async Task<ExchangeEntity> GetExchangeAsync(Currency CurrencyFrom, Currency CurrencyTo)
        {
            var rate = await _db.exchangeEntities
                .SingleOrDefaultAsync(x => x.currencyFrom == CurrencyFrom && x.currencyTo == CurrencyTo);
            if (rate == null) throw new Exception("Not Found rate fom these currences!");
            return rate;
        }

        public async Task<CardEntity?> GetCardByNumberAndPinAsync(string cardNumber, string pinCode)
        {
            var card = await _db.CardEntities
                .Include(x => x.AccountEntity)
                .ThenInclude(y => y.CustomerEntity)
                .SingleOrDefaultAsync(x => x.CardNumber == cardNumber && x.PIN == pinCode);
            return card ?? null;
        }

        public async Task AddCardToDbAsync(CardEntity card)
        {
            await _db.CardEntities.AddAsync(card);
        }

        public async Task<UserEntity?> GetCardOwnerByAccountIdAsync(int id)
        {
            var result = await _db.AccountEntities
                .Include(x => x.CustomerEntity)
                .SingleOrDefaultAsync(c => c.AccountEntityId == id);
            if (result == null) throw new Exception("Account Not Found in db!");
            var customer = GetCustomerById(result.CustomerEntityId);
            return customer.Result;
        }

        public async Task<UserEntity> GetCustomerById(int id)
        {
            var customer = await _db.UserEntities.FirstOrDefaultAsync(x => x.Id == id);
            return customer ?? throw new Exception("Customer Not Found!");
        }

        public async Task<CardEntity> GetCardById(int id)
        {
            var card = await _db.CardEntities.SingleOrDefaultAsync(x => x.CardEntityId == id);
            return card ?? throw new Exception("Card Not Found!");
        }

        public async Task<CardEntity> GetCardByNumberAsync(string cardNumber)
        {
            var card = await _db.CardEntities.SingleOrDefaultAsync(c => c.CardNumber == cardNumber);
            return card ?? throw new Exception("Card Not Found!");
        }

        public async Task<bool> UpdateCardPinAsync(string cardNumber, string newPinCode)
        {
            var card = await _db.CardEntities.SingleOrDefaultAsync(c => c.CardNumber == cardNumber);
            if (card == null)
                return false;
            card.PIN = newPinCode;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

