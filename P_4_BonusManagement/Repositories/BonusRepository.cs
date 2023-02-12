using System;
using System.Collections.Generic;
using P_4_BonusManagement.Data;
using System.Threading.Tasks;
using P_4_BonusManagement.Data.Entity;
using Microsoft.EntityFrameworkCore;
using P_4_BonusManagement.Models;
using P_4_BonusManagement.Models.Requests;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using P_4_BonusManagement.Repositories;
using System.Text.RegularExpressions;
using System.Linq;

namespace P_4_BonusManagement.Repositories
{
    public interface IBonusRepository
    {
        Task<IEnumerable<BonusEntity>> GetBonusesAsync();
        Task<NewEmptyClass> SearchBonusesByDateAsync(SearchBonusByDateRequest request);

        Task<BonusEntity> CreateBonusAsync(CreateBonusRequest request);
        Task<BonusEntity> TwoCreateBonusAsync(CreateBonusRequest request);

        Task<List<EmployeeEntity>> GetTopEmployeesWithMostBonuses();
        Task<List<NClass>> GetTopRecomendator();

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
            var result = await _db.BonusEntities.ToListAsync();
            return result;
        }

        public async Task<NewEmptyClass> SearchBonusesByDateAsync(SearchBonusByDateRequest request)
        {
            IQueryable<BonusEntity> _query = _db.BonusEntities;

            if (!string.IsNullOrEmpty(request.FromDate.ToString()))
            {
                _query = _query
                    .Where(e => e.IssueDate > request.FromDate
                            && e.IssueDate < request.BeforeDate);
            }
            var countBonuses = _query.Count();
            var sumBonuses = _query.Sum(e => e.BonusAmount);

            var emptyClass = new EmptyClass();
            emptyClass.BonusCount = countBonuses;
            emptyClass.BonusesSum = sumBonuses;

            var passData = new NewEmptyClass();

            passData.emptyClass = emptyClass;
            passData.bonusEntity = _query;

            return passData;
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

        //public async Task<List<NClass>> GetTopRecomendator()
        //{

            //var result = (from ee in _db.EmployeeEntities
            //              join be in _db.BonusEntities on ee.EmployeeEntityId equals be.EmployeeEntityId
            //              where ee.RecommenderId != 0
            //              group new { ee, be } by ee.RecommenderId into g
            //              select new
            //              {
            //                  recId = g.Key,
            //                  RecName = g.First().ee.FirstName,
            //                  BB = g.Sum(x => x.be.BonusAmount),
            //                  CC = g.Count()
            //              }).OrderBy(x => x.BB);

            //    var result = (from employee in _db.EmployeeEntities
            //                  join bonus in _db.BonusEntities on employee.EmployeeEntityId equals bonus.EmployeeEntityId
            //                  where employee.RecommenderId != 0
            //                  group new { employee, bonus } by employee.RecommenderId into g
            //                  select new
            //                  {
            //                      recId = g.Key,
            //                      RecName = g.First().employee.FirstName,

            //                      CC = g.Count(),
            //                      BB = g.Sum(x => x.bonus.BonusAmount)
            //                  }).OrderByDescending(x => x.BB).ThenByDescending(d => d.CC);

            //    List<NClass> allResults = new List<NClass>();
            //    foreach (var item in result)
            //    {
            //        var dd = new NClass() { Name = item.RecName, BonusAmount = item.BB, CountBonus = item.CC, RecomendatorId = item.recId };
            //allResults.Add(dd);
            //    }
            //    return allResults;
            //}

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

        public async Task<StrangClass> AddBonusEntity(int employeeId, double amount)
        {
            var employee = await _db.EmployeeEntities.FirstOrDefaultAsync(e => e.EmployeeEntityId == employeeId);
            if(employee == null)
                throw new GiorgisException("aseti momxmarebeli ar arsebobs - AddBonusEntity");
            var bonus = new BonusEntity() { EmployeeEntityId = employeeId, BonusAmount = amount, IssueDate = DateTime.Now};
            var result = await _db.BonusEntities.AddAsync(bonus);
            await SaveChangesAsync();
            var str = new StrangClass() {RecomId = employee.RecommenderId, NewAmount = amount/2, bonusId = bonus.BonusEntityId };
            return str;
        }

        public async Task<BonusEntity> CreateBonusAsync(CreateBonusRequest request)
        {
            var employee = await _db.EmployeeEntities.FirstOrDefaultAsync(e => e.EmployeeEntityId == request.EmployeeId);
            if (employee == null)
                throw new EmployeeNotFoundException(request.EmployeeId, "aseti momxmarebeli ar arsebobs - CreateBonusAsync");

            if (request.BonusAmount > 3 * employee.Salary || request.BonusAmount < employee.Salary / 2)
                throw new GiorgisException("Giorgis eqsepSeni, bonusebi diapazonSI ar jdeba");

            var bonus1 = await AddBonusEntity(employee.EmployeeEntityId, request.BonusAmount);
            if (bonus1.RecomId != 0)
            {
                var bonus2 = await AddBonusEntity(bonus1.RecomId, bonus1.NewAmount);
                if (bonus2.RecomId != 0)
                {
                    var bonus3 = AddBonusEntity(bonus2.RecomId, bonus2.NewAmount);
                }
            }
            //await SaveChangesAsync();
            var result = await _db.BonusEntities.FirstOrDefaultAsync(x => x.BonusEntityId == bonus1.bonusId);
            return result;
        }

        // ეს მუშაობს ზედა ვარიანტი იგივეა და არ მუშაობს. ფუნქციები ცალკეა გატანილი უბრალოდ.
        public async Task<BonusEntity> TwoCreateBonusAsync(CreateBonusRequest request)
        {
            var employee = await _db.EmployeeEntities
                .Include(e => e.BonusEntities)
                .FirstOrDefaultAsync(e => e.EmployeeEntityId == request.EmployeeId);

            if(employee == null)
                throw new GiorgisException("Giorgis Exsepsheni - employee1 is null in TwoCreateBonusAsync");
            if (request.BonusAmount > 3 * employee.Salary || request.BonusAmount < employee.Salary / 2)
                throw new GiorgisException("Giorgis eqsepSeni, bonusebi diapazonSI ar jdeba");
            var bonus1 = new BonusEntity() { EmployeeEntityId = request.EmployeeId, BonusAmount = request.BonusAmount, IssueDate = DateTime.Now };
            var result = await _db.BonusEntities.AddAsync(bonus1);
            employee.BonusEntities.Add(bonus1);
            // ბონუსებს ვერ ვამატებ მომხმარებლის ბონუსების სიაში?????????????????????/
            // და საერთოდ რამე უნდა დაემატოს აქ, თუ უბრალობ ფორინკეით არის დაკავშირებული
            _db.EmployeeEntities.Update(employee);
            //await SaveChangesAsync();
            if(employee.RecommenderId != 0)
            {
                var employee2 = await _db.EmployeeEntities.FirstOrDefaultAsync(e => e.EmployeeEntityId == employee.RecommenderId);
                if (employee2 == null)
                    throw new GiorgisException("employee2 is null in TwoCreateBonusAsync");
                var bonus2 = new BonusEntity() { EmployeeEntityId = employee2.EmployeeEntityId, BonusAmount = request.BonusAmount / 2, IssueDate = DateTime.Now };
                var result2 = await _db.BonusEntities.AddAsync(bonus2);
                employee2.BonusEntities.Add(bonus2);
                _db.EmployeeEntities.Update(employee2);
                //await SaveChangesAsync();

                if (employee2.RecommenderId != 0)
                {
                    var employee3 = await _db.EmployeeEntities.FirstOrDefaultAsync(e => e.EmployeeEntityId == employee2.RecommenderId);
                    if (employee3 == null)
                        throw new GiorgisException("employee3 is null in TwoCreateBonusAsync");
                    var bonus3 = new BonusEntity() { EmployeeEntityId = employee3.EmployeeEntityId, BonusAmount = request.BonusAmount / 4, IssueDate = DateTime.Now };
                    var result3 = await _db.BonusEntities.AddAsync(bonus3);
                    employee3.BonusEntities.Add(bonus3);
                    _db.EmployeeEntities.Update(employee3);
                    //await SaveChangesAsync();
                }
            }
            return result.Entity;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
