using System.Diagnostics;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Models.Requests.Card;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CredoProject.Core.Repositories
{
    public interface IReportsRepository
    {
        Task<IEnumerable<UserEntity>> GetUsersInRoleUserAsync();
        Task<List<TransactionEntity>> GetTransactionsAsync();
    }

    public class ReportsRepository : IReportsRepository
    {

        private readonly CredoDbContext _db;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IEnumerable<UserEntity> _usersRoleApiUser;

        public ReportsRepository (CredoDbContext db, UserManager<UserEntity> userManager)
        {
            _db = db;
            _userManager = userManager;
            _usersRoleApiUser = _userManager.GetUsersInRoleAsync("api-user").Result;
        }

        public async Task<IEnumerable<UserEntity>> GetUsersInRoleUserAsync()
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync("api-user");
            return usersInRole;
        }

        public async Task<List<TransactionEntity>> GetTransactionsAsync()
        {
            return await _db.TransactionEntities.ToListAsync();
        }

    }
}
