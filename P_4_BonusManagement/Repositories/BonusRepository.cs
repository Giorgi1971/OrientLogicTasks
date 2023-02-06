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
            var result = await _db.Bonusies.ToListAsync();
            return result;
        }


        public async Task<NewEmptyClass> SearchBonusesByDateAsync(SearchBonusByDateRequest request)
        {
            IQueryable<BonusEntity> _query = _db.Bonusies;

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
            return await _db.Employees
            .Include(e => e.Bonuses)
            .OrderByDescending(e => e.Bonuses.Sum(b => b.BonusAmount))
            .Take(10)
            .ToListAsync();
        }

        public async Task<List<NClass>> GetTopRecomendator()
        {
            var result = _db.Employees
                         .Where(e => e.RecommenderId != 0)
                         .Join(_db.Bonusies, e => e.Id, b => b.EmployeeId, (e, b) => new { e, b })
                         .GroupBy(x => x.e.RecommenderId)
                         .Select(g => new
                         {
                             RecommenderId = g.Key,
                             ck = g.Count(),
                             BBA = g.Sum(x => x.b.BonusAmount)
                         })
                         .Take(10)
                         .OrderByDescending(x => x.ck)
                         .ThenByDescending(x => x.BBA);

            List<NClass> allResults = new List<NClass>();
            foreach (var item in result)
            {
                var dd = new NClass() { Amount = item.BBA, RecomendatorId = item.ck };
                allResults.Add(dd);
            }
            return allResults;
        }

        //public async Task<List<NClass>> GetTopRecomendator()
        //{
        //    var result = await (from e in _db.Bonusies
        //                        join d in _db.Employees on e.EmployeeId equals d.Id
        //                        where d.RecommenderId != 0
        //                        select new { Bonuses = e.BonusAmount, RecomedatorId = d.RecommenderId }).ToListAsync();

        //    List<NClass> allResults = new List<NClass>();
        //    foreach (var item in result)
        //    {
        //        var dd = new NClass() { Amount = item.Bonuses, RecomendatorId = item.RecomedatorId };
        //        allResults.Add(dd);
        //    }
        //    return allResults;
        //}

        public async Task<StrangClass> AddBonusEntity(int employeeId, double amount)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if(employee == null)
                throw new GiorgisException("aseti momxmarebeli ar arsebobs - AddBonusEntity");
            var bonus = new BonusEntity() { EmployeeId = employeeId, BonusAmount = amount, IssueDate = DateTime.Now};
            var result = await _db.Bonusies.AddAsync(bonus);
            await SaveChangesAsync();
            var strang = new StrangClass() {RecomId = employee.RecommenderId, NewAmount = amount/2, bonusId = bonus.Id };
            return strang;
        }

        public async Task<BonusEntity> CreateBonusAsync(CreateBonusRequest request)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == request.EmployeeId);
            if (employee == null)
                throw new GiorgisException("aseti momxmarebeli ar arsebobs - CreateBonusAsync");

            if (request.BonusAmount > 3 * employee.Salary || request.BonusAmount < employee.Salary / 2)
                throw new GiorgisException("Giorgis eqsepSeni, bonusebi diapazonSI ar jdeba");

            var bonus1 = AddBonusEntity(employee.Id, request.BonusAmount);
            if (bonus1.Result.RecomId != 0)
            {
                var bonus2 = AddBonusEntity(bonus1.Result.RecomId, bonus1.Result.NewAmount);
                if (bonus2.Result.RecomId != 0)
                {
                    var bonus3 = AddBonusEntity(bonus2.Result.RecomId, bonus2.Result.NewAmount);
                }
            }
            //await SaveChangesAsync();
            var result = await _db.Bonusies.FirstOrDefaultAsync(x => x.Id == bonus1.Result.bonusId);
            return result;
        }

        // ეს მუშაობს ზედა ვარიანტი იგივეა და არ მუშაობს. ფუნქციები ცალკეა გატანილი უბრალოდ.
        public async Task<BonusEntity> TwoCreateBonusAsync(CreateBonusRequest request)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == request.EmployeeId);
            if (request.BonusAmount > 3 * employee.Salary || request.BonusAmount < employee.Salary / 2)
                throw new GiorgisException("Giorgis eqsepSeni, bonusebi diapazonSI ar jdeba");
            var bonus1 = new BonusEntity() { EmployeeId = request.EmployeeId , BonusAmount = request.BonusAmount, IssueDate = DateTime.Now };
            var result = await _db.Bonusies.AddAsync(bonus1);
            // ბონუსებს ვერ ვამატებ მომხმარებლის ბონუსების სიაში?????????????????????/
            employee.Bonuses.Add(bonus1);
            _db.Employees.Update(employee);
            //await SaveChangesAsync();
            if(employee.RecommenderId != 0)
            {
                var employee2 = await _db.Employees.FirstOrDefaultAsync(e => e.Id == employee.RecommenderId);
                var bonus2 = new BonusEntity() { EmployeeId = employee2.Id, BonusAmount = request.BonusAmount / 2, IssueDate = DateTime.Now };
                var result2 = await _db.Bonusies.AddAsync(bonus2);
                employee2.Bonuses.Add(bonus2);
                _db.Employees.Update(employee2);

                //await SaveChangesAsync();
                if (employee2.RecommenderId != 0)
                {
                    var employee3 = await _db.Employees.FirstOrDefaultAsync(e => e.Id == employee2.RecommenderId);
                    var bonus3 = new BonusEntity() { EmployeeId = employee3.Id, BonusAmount = request.BonusAmount / 4, IssueDate = DateTime.Now };
                    var result3 = await _db.Bonusies.AddAsync(bonus3);
                    employee3.Bonuses.Add(bonus3);
                    _db.Employees.Update(employee3);

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

