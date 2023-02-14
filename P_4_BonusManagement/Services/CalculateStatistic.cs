using System;
using System.Linq;
using P_4_BonusManagement.Data;
using P_4_BonusManagement.Data.Entity;
using P_4_BonusManagement.Repositories;
using P_4_BonusManagement.Responce;

namespace P_4_BonusManagement.Services
{

    public interface ICalculateStatistic
    {
        Task<List<NClass>> GetTopRecomendatorCount(List<BonusEntity> query);
        Task<List<ResponceTopRecomendator>> CalculateTopRecomendator(List<EmployeeEntity> _employees, List<BonusEntity> _bonuses);
        Task<List<ResponceTopBonused>> CalculateTopBonused(List<EmployeeEntity> _employees, List<BonusEntity> _bonuses);
    }

    public class CalculateStatistic : ICalculateStatistic
    {

        private readonly IStatisticRepository _statistic;
        public CalculateStatistic()
        {
        }


        public CalculateStatistic(IStatisticRepository db)
        {
            _statistic = db;
        }

        
        public async Task<List<ResponceTopBonused>> CalculateTopBonused(List<EmployeeEntity> _employees, List<BonusEntity> _bonuses)
        {
            Dictionary<int, ResponceTopRecomendator> dictAll = new Dictionary<int, ResponceTopRecomendator>() {};
            List<ResponceTopRecomendator> result = new List<ResponceTopRecomendator> { };

            var query =
                from bon in _bonuses
                join emo in _employees on bon.EmployeeEntityId equals emo.EmployeeEntityId
                group bon by bon.EmployeeEntityId into newgroup 
                select new ResponceTopBonused
                {
                    EmoloyeeId = newgroup.Key,
                    EmoloyeeName = newgroup.First().EmployeeEntity.FirstName,
                    TotalAmount = newgroup.Sum(m => m.BonusAmount)
                }
                ;

            var res = query.OrderByDescending(x => x.TotalAmount).Take(2).ToList();
            return res;
        }


        public async Task<List<ResponceTopRecomendator>> CalculateTopRecomendator(List<EmployeeEntity> _employees, List<BonusEntity> _bonuses)
        {
            Dictionary<int, ResponceTopRecomendator> dictAll = new Dictionary<int, ResponceTopRecomendator>() { };
            List<ResponceTopRecomendator> result = new List<ResponceTopRecomendator> { };
            foreach (var bon in _bonuses)
            {
                foreach (var emp in _employees)
                {
                    if (bon.EmployeeEntityId == emp.EmployeeEntityId)
                    {
                        if (emp.RecommenderId == 0)
                            continue;
                        if (!dictAll.ContainsKey(emp.RecommenderId))
                        {
                            dictAll[emp.RecommenderId] = new ResponceTopRecomendator();
                            dictAll[emp.RecommenderId].Name = _employees.FirstOrDefault(x => x.EmployeeEntityId == emp.RecommenderId).FirstName;
                            dictAll[emp.RecommenderId].RecId = emp.RecommenderId;
                            dictAll[emp.RecommenderId].ReferalAmount = bon.BonusAmount;
                            result.Add(dictAll[emp.RecommenderId]);
                        }
                        else
                            dictAll[emp.RecommenderId].ReferalAmount += bon.BonusAmount;
                    }
                }
            }

            var rez3 = result.OrderByDescending(x => x.ReferalAmount).Take(2).ToList();
            return rez3;
        }


        public async Task<List<NClass>> GetTopRecomendatorCount(List<BonusEntity> query)
        {
            var dd = query
                .Where(m => m.EmployeeEntity.RecommenderId != 0)
                .GroupBy(m => m.EmployeeEntity.RecommenderId)
                .Select(g => new NClass
                {
                    RecomendatorId = g.Key,
                    CountBonus = g.Count(),
                    BonusAmount = g.Sum(x => x.BonusAmount)
                })
                .Take(10)
                .OrderByDescending(x => x.BonusAmount)
                .ThenByDescending(x => x.CountBonus)
                .ToList();
            //List<NClass> nClasses = new List<NClass>();
            //foreach (var item in dd)
            //{
            //    nClasses.Add(new NClass() { BonusAmount = item.BonusAmount, CountBonus = item.CountBonus, RecomendatorId = item.RecomendatorId });
            //}
           
            return dd;
        }
    }
}
