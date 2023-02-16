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
        List<BonusEntity> GetJoinedData();
    }

    public class StatisticRepository :IStatisticRepository
    {
        private readonly AppDbContext _db;

        public StatisticRepository(AppDbContext db)
        {
            _db = db;
        }

        public List<BonusEntity> GetJoinedData()
        {
            var query = _db.BonusEntities
                .Include(s => s.EmployeeEntity)
                .ToList();

            return query;
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
                         .Select(g => new NClass
                         {
                             RecomendatorId = g.Key,
                             CountBonus = g.Count(),
                             BonusAmount = g.Sum(x => x.b.BonusAmount)
                         })
                         .Take(10)
                         .OrderByDescending(x => x.BonusAmount)
                         .ThenByDescending(x => x.CountBonus)
                         .ToListAsync();

            return result;
        }
    }
}
