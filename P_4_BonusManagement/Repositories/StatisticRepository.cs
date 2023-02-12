using System;
using Microsoft.EntityFrameworkCore;
using P_4_BonusManagement.Data;
using P_4_BonusManagement.Data.Entity;

namespace P_4_BonusManagement.Repositories
{
    public interface IStatisticRepository
    {
        Task<List<EmployeeEntity>> GetTopEmployeesWithMostBonuses();
        Task<List<NClass>> GetTopRecomendator();


    }

    public class StatisticRepository :IStatisticRepository
    {
        private readonly AppDbContext _db;

        public StatisticRepository(AppDbContext db)
        {
            _db = db;
        }

        //  ფუნქცია, რომელიც ბაზაში არაფერს წერს, ასინქრონული უნდა იყოს თუ არა?
        public async Task<List<EmployeeEntity>> GetTopEmployeesWithMostBonuses()
        {
            return await _db.EmployeeEntities
                .Include(e => e.BonusEntities)
                .OrderByDescending(e => e.BonusEntities.Sum(b => b.BonusAmount))
                .Take(10)
                .ToListAsync();
        }

        public async Task<List<NClass>> GetTopRecomendator()
        {
            var result = await _db.EmployeeEntities
                         .Where(e => e.RecommenderId != 0)
                         .Join(_db.BonusEntities, e => e.EmployeeEntityId, b => b.EmployeeEntityId, (e, b) => new { e, b })
                         .GroupBy(x => x.e.RecommenderId)
                         .Select(g => new
                         {
                             RecommenderId = g.Key,
                             ck = g.Count(),
                             BBA = g.Sum(x => x.b.BonusAmount)
                         })
                         .Take(10)
                         .OrderByDescending(x => x.BBA)
                         .ThenByDescending(x => x.ck)
                         .ToListAsync();

            List<NClass> allResults = new List<NClass>();
            foreach (var item in result)
            {
                var dd = new NClass() { BonusAmount = item.BBA, CountBonus = item.ck, RecomendatorId = item.RecommenderId };
                allResults.Add(dd);
            }
            return allResults;
        }




    }
}

