using System;
using System.Collections.Generic;
using P_4_BonusManagement.Data;
using System.Threading.Tasks;
using P_4_BonusManagement.Data.Entity;
using Microsoft.EntityFrameworkCore;
using P_4_BonusManagement.Models;
using P_4_BonusManagement.Models.Requests;
using System.Reflection;

namespace P_4_BonusManagement.Repositories
{
    public interface IBonusRepository
    {
        Task<IEnumerable<BonusEntity>> GetBonusesAsync();
        Task<NewEmptyClass> SearchBonusesByDateAsync(SearchBonusByDateRequest request);
        Task SaveChangesAsync();
    }

    public class BonusRepository : IBonusRepository
    {
        private readonly AppDbContext _db;

        public BonusRepository(AppDbContext db)
        {
            _db = db;
        }


        public async Task<IEnumerable<BonusEntity>> GetBonusesAsync()
        {
            return await _db.Bonusies.ToListAsync();
        }


        public async Task<NewEmptyClass> SearchBonusesByDateAsync(SearchBonusByDateRequest request)
        {
            IQueryable<BonusEntity> query = _db.Bonusies;

            if (!string.IsNullOrEmpty(request.FromDate.ToString()))
            {
                query = query
                    .Where(e => e.IssueDate > request.FromDate
                            && e.IssueDate < request.BeforeDate);
            }
            var countBonuses = query.Count();
            var sumBonuses = query.Sum(e => e.BonusAmount);

            // ასე მინდა რომ შემეძლოს გაკეთება:
            //var result = _db.Bonusies
            //    .Where(x => x.IssueDate > request.FromDate
            //                && x.IssueDate < request.BeforeDate)
            //    .Select(x => new
            //    {
            //        Count = _db.Bonusies.Count(),
            //        Sum = _db.Bonusies.Sum(y => y.BonusAmount)
            //    })
            //    .FirstOrDefault();


            var emptyClass = new EmptyClass();
            emptyClass.BonusCount = countBonuses;
            emptyClass.BonusesSum = sumBonuses;

            var passData = new NewEmptyClass();

            passData.emptyClass = emptyClass;
            passData.bonusEntity = query;

            return passData;
        }


        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}

