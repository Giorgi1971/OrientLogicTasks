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


        public StrangClass AddBonusEntity(int employeeId, double amount)
        {
            var employee = _db.EmployeeEntities.FirstOrDefault(e => e.EmployeeEntityId == employeeId);
            if (employee == null)
                throw new GiorgisException("Gio Gio aseti momxmarebeli ar arsebobs (BonusRepository) - AddBonusEntity");
            var bonus = new BonusEntity() { EmployeeEntityId = employeeId, BonusAmount = amount, IssueDate = DateTime.Now };
            var result = _db.BonusEntities.Add(bonus);
            _db.SaveChanges();
            var str = new StrangClass() { RecomId = employee.RecommenderId, NewAmount = amount / 2, bonusId = bonus.BonusEntityId };
            return str;
        }


        public async Task<BonusEntity> CreateBonusAsync(CreateBonusRequest request)
        {
            var employee = _db.EmployeeEntities.FirstOrDefault(e => e.EmployeeEntityId == request.EmployeeId);
            if (employee == null)
                throw new EmployeeNotFoundException(request.EmployeeId, "aseti momxmarebeli ar arsebobs (BonusRepository) - CreateBonusAsync");

            if (request.BonusAmount > 3 * employee.Salary || request.BonusAmount < employee.Salary / 2)
                throw new GiorgisException("Bonusebi diapazonSI ar jdeba (BonusRepository)");

            var bonus1 = AddBonusEntity(employee.EmployeeEntityId, request.BonusAmount);
            if (bonus1.RecomId != 0)
            {
                var bonus2 = AddBonusEntity(bonus1.RecomId, bonus1.NewAmount);
                if (bonus2.RecomId != 0)
                {
                    var bonus3 = AddBonusEntity(bonus2.RecomId, bonus2.NewAmount);
                }
            }
            var result = _db.BonusEntities.FirstOrDefaultAsync(x => x.BonusEntityId == bonus1.bonusId);
            return await result;
        }


        public async Task<BonusEntity> TwoCreateBonusAsync(CreateBonusRequest request)
        {
            var employee = await _db.EmployeeEntities
                .Include(e => e.BonusEntities)
                .FirstOrDefaultAsync(e => e.EmployeeEntityId == request.EmployeeId);

            if(employee == null)
                throw new GiorgisException("Employee1 is null in TwoCreateBonusAsync (BonusRepository)");
            if (request.BonusAmount > 3 * employee.Salary || request.BonusAmount < employee.Salary / 2)
                throw new Exception("Bonusebi diapazonshi ar jdeba (BonusRepository)");
            var bonus1 = new BonusEntity() { EmployeeEntityId = request.EmployeeId, BonusAmount = request.BonusAmount, IssueDate = DateTime.Now };
            var result = await _db.BonusEntities.AddAsync(bonus1);

            if(employee.RecommenderId != 0)
            {
                var employee2 = await _db.EmployeeEntities.FirstOrDefaultAsync(e => e.EmployeeEntityId == employee.RecommenderId);
                if (employee2 == null)
                    throw new GiorgisException($"employee2 (Id: {employee.RecommenderId}) is null in TwoCreateBonusAsync");
                var bonus2 = new BonusEntity() { EmployeeEntityId = employee2.EmployeeEntityId, BonusAmount = request.BonusAmount / 2, IssueDate = DateTime.Now };
                var result2 = await _db.BonusEntities.AddAsync(bonus2);

                if (employee2.RecommenderId != 0)
                {
                    var employee3 = await _db.EmployeeEntities.FirstOrDefaultAsync(e => e.EmployeeEntityId == employee2.RecommenderId);
                    if (employee3 == null)
                        throw new GiorgisException($"employee3 (Id: {employee2.RecommenderId}) is null in TwoCreateBonusAsync");
                    var bonus3 = new BonusEntity() { EmployeeEntityId = employee3.EmployeeEntityId, BonusAmount = request.BonusAmount / 4, IssueDate = DateTime.Now };
                    var result3 = await _db.BonusEntities.AddAsync(bonus3);
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
