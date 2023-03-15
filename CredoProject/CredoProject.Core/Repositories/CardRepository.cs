//using System;
//using CredoProject.Core.Db;
//using CredoProject.Core.Db.Entity;
//using Microsoft.EntityFrameworkCore;

//namespace CredoProject.Core.Repositories
//{

//    public interface ICardRepository
//    {
//        Task AddCardToDbAsync(CardEntity card);
//        Task<CustomerEntity> GetCardOwnerByAccountIdAsync(int id);
//        Task SaveChangesAsync();
//    }

//    public class CardRepository : ICardRepository
//    {

//        private readonly CredoDbContext _db;

//        public CardRepository(CredoDbContext db)
//        {
//            _db = db;
//        }

//        public async Task AddCardToDbAsync(CardEntity card)
//        {
//            await _db.CardEntities.AddAsync(card);
//        }


//        public async Task<CustomerEntity> GetCardOwnerByAccountIdAsync(int id)
//        {
//            var result = await _db.AccountEntities
//                .Include(x => x.CustomerEntity)
//                .SingleOrDefaultAsync(c => c.AccountEntityId == id);
//            var customer = GetCustomerById(result.CustomerEntityId);
//            return customer.Result;
//        }

//        public async Task<CustomerEntity> GetCustomerById(int id)
//        {
//            var customer = await _db.CustomerEntities.FirstOrDefaultAsync(x => x.CustomerEntityId == id);
//            return customer ?? throw new Exception("Customer Not Found!");
//        }


//        public async Task SaveChangesAsync()
//        {
//            await _db.SaveChangesAsync();
//        }

//    }
//}

