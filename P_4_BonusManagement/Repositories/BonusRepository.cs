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

namespace P_4_BonusManagement.Repositories
{
    public interface IBonusRepository
    {
        Task<IEnumerable<BonusEntity>> GetBonusesAsync();
        Task<NewEmptyClass> SearchBonusesByDateAsync(SearchBonusByDateRequest request);
        Task SaveChangesAsync();
        Task<List<EmployeeEntity>> GetTopEmployeesWithMostBonuses();
        Task<BonusEntity> CreateBonusAsync(CreateBonusRequest request);
        Task<BonusEntity> OneCreateBonusAsync(CreateBonusRequest request);
        Task<BonusEntity> TwoCreateBonusAsync(CreateBonusRequest request);

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

        // ფუნქცია, რომელიც ბაზაში არაფერს წერს, ასინქრონული უნდა იყოს თუ არა?
        public async Task<List<EmployeeEntity>> GetTopEmployeesWithMostBonuses()
        {
            return await _db.Employees
            .Include(e => e.Bonuses)
            .OrderByDescending(e => e.Bonuses.Sum(b => b.BonusAmount))
            .Take(10)
            .ToListAsync();
        }

        public async Task<StrangClass> AddBonusEntity(int employeeId, double amount)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (amount > 3 * employee.Salary || amount < employee.Salary / 2)
                throw new GiorgisException("Giorgis eqsepSeni, bonusebi diapazonSI ar jdeba");
            // ვამატებთ ბონუსს რექვესტის თანახმად, ვამატებთ ბონუსების სიაში რეკომენდატორთან
            var bonus1 = new BonusEntity();
            bonus1.EmployeeId = employeeId;
            bonus1.BonusAmount = amount;
            bonus1.IssueDate = DateTime.Now;
            var result = await _db.Bonusies.AddAsync(bonus1);
            //employee.Bonuses.Add(bonus1);
            await SaveChangesAsync();
            var strang = new StrangClass();
            strang.RecomId = employee.RecommenderId;
            strang.NewAmount = amount / 2;
            strang.bonusId = bonus1.Id;
            return strang;
        }

        // ეს ვარიანტი ჯერ-ჯერობით არ მუშაობს.
        public async Task<BonusEntity> CreateBonusAsync(CreateBonusRequest request)
        {
            // ვამოწმებთ ბონუსის ოდენობას
            var bonus1 = AddBonusEntity(request.EmployeeId, request.BonusAmount);
            // ვამოწმებთ ყავს თუ არა რეკომენდატორი, თუ ყავს ვაძლევთ მას ბონუსის ნახევარს. ვამატებთ ბონუსების სიაში რეკომენდატორთან
            if (bonus1.Result.RecomId != 0)
            {
                var bonus2 = AddBonusEntity(bonus1.Result.RecomId, bonus1.Result.NewAmount);
                // ახლა ვამოწმებთ ამ რეკომენდატორს - ყავს თუ არა რეკომენდატორი, თუ ყავს ვაძლევთ მას ბონუსის ნახევარს. ვამატებთ ბონუსების სიაში რეკომენდატორთან
                if (bonus2.Result.RecomId != 0)
                {
                    var bonus3 = AddBonusEntity(bonus2.Result.RecomId, bonus2.Result.NewAmount);
                }
            }
            var result = await _db.Bonusies.FirstOrDefaultAsync(x => x.Id == bonus1.Result.bonusId);
            return result;
            // ვაბრუნებთ პირველი ბონუსის ჩანაწერს
        }


        public async Task<BonusEntity> OneCreateBonusAsync(CreateBonusRequest request)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == request.EmployeeId);
            if (request.BonusAmount > 3 * employee.Salary || request.BonusAmount < employee.Salary / 2)
                throw new GiorgisException("Giorgis eqsepSeni, bonusebi diapazonSI ar jdeba");
            var bonus1 = new BonusEntity();
            bonus1.EmployeeId = request.EmployeeId;
            bonus1.BonusAmount = request.BonusAmount;
            bonus1.IssueDate = DateTime.Now;
            var result = await _db.Bonusies.AddAsync(bonus1);
            await SaveChangesAsync();
            return result.Entity;
        }

        public async Task<BonusEntity> TwoCreateBonusAsync(CreateBonusRequest request)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == request.EmployeeId);
            if (request.BonusAmount > 3 * employee.Salary || request.BonusAmount < employee.Salary / 2)
                throw new GiorgisException("Giorgis eqsepSeni, bonusebi diapazonSI ar jdeba");
            var bonus1 = new BonusEntity() { EmployeeId = request.EmployeeId , BonusAmount = request.BonusAmount };
            var result = await _db.Bonusies.AddAsync(bonus1);
            await SaveChangesAsync();
            if(employee.RecommenderId != 0)
            {
                var employee2 = await _db.Employees.FirstOrDefaultAsync(e => e.Id == employee.RecommenderId);
                var amount2 = request.BonusAmount / 2;
                if (amount2 > 3 * employee.Salary || amount2 < employee.Salary / 2)
                    throw new GiorgisException("Giorgis 2222 eqsepSeni, bonusebi diapazonSI ar jdeba");
                var bonus2 = new BonusEntity() { EmployeeId = employee2.Id, BonusAmount = amount2 };
                var result2 = await _db.Bonusies.AddAsync(bonus2);
                await SaveChangesAsync();
                if (employee2.RecommenderId != 0)
                {
                    var employee3 = await _db.Employees.FirstOrDefaultAsync(e => e.Id == employee2.RecommenderId);
                    var amount3 = amount2 / 2;
                    if (amount3 > 3 * employee.Salary || amount3 < employee.Salary / 2)
                        throw new GiorgisException("Giorgis 33333 eqsepSeni, bonusebi diapazonSI ar jdeba");
                    var bonus3 = new BonusEntity() { EmployeeId = employee3.Id, BonusAmount = amount3 };
                    var result3 = await _db.Bonusies.AddAsync(bonus3);
                    await SaveChangesAsync();
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

