﻿using System;
using P_4_BonusManagement.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using P_4_BonusManagement.Data.Entity;
using Microsoft.EntityFrameworkCore;
using P_4_BonusManagement.Models.Requests;
using System.Reflection;

namespace P_4_BonusManagement.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> GetEmployees();
        Task<EmployeeEntity> GetEmployee(int employeeId);
        Task<EmployeeEntity> AddEmployee(CreateEmployeeRequest request);
        Task<EmployeeEntity> UpdateEmployee(int id, UpdateEmployeeRequest request);
        Task<EmployeeEntity> DeleteEmployee(int employeeId);
        Task SaveChangesAsync();
        Task<IEnumerable<EmployeeEntity>> Search(string name);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _db;

        public EmployeeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetEmployees()
        {
            return await _db.Employees.ToListAsync();
        }

        public async Task<EmployeeEntity> GetEmployee(int employeeId)
        {
            var result = await _db.Employees
                .FirstOrDefaultAsync(e => e.Id == employeeId);
            // ეს სწორად მიწერია?? ექსეპშენს რომ გაისვრის მერე რა მოხდება??? return null ხომ არ უნდა??;
            if (result != null)
                return result;
            throw new GiorgisException($"Get Employee by Id Failed. Entity with id {employeeId} not found.");
        }

        public async Task<EmployeeEntity> AddEmployee(CreateEmployeeRequest request)
        {

            var empolyee = new EmployeeEntity();
            empolyee.FirstName = request.FirstName;
            empolyee.LastName = request.LastName;
            empolyee.Salary = request.Salary;
            empolyee.RecommenderId = request.RecommenderId;
            empolyee.HiringDate = DateTime.Now;
            empolyee.PersonalNumber = request.PersonalNumber;

            var result = await _db.Employees.AddAsync(empolyee);
            return result.Entity;
        }

        public async Task<EmployeeEntity> UpdateEmployee(int id, UpdateEmployeeRequest request)
        {
            var result = await _db.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (result != null)
            {
                result.LastName = request.LastName;
                result.Salary = request.Salary;
                result.HiringDate = request.HiringDate;
                //await _db.Employees.Update(result);

                return result;
            }
            // აქაც რომელი უნდა დავწერო???
            //throw new GiorgisException($"Create Employee Failed. Entity with id {request.Id} not found.");
            return null;
        }

        public async Task<EmployeeEntity> DeleteEmployee(int employeeId)
        {
            var result = await _db.Employees
                .FirstOrDefaultAsync(e => e.Id == employeeId);
            if (result != null)
            {
                _db.Employees.Remove(result);
            }
            return result;
        }

        public async Task<IEnumerable<EmployeeEntity>> Search(string name)
        {
            IQueryable<EmployeeEntity> query = _db.Employees;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name)
                            || e.LastName.Contains(name));
            }

            return await query.ToListAsync();
        }


        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}