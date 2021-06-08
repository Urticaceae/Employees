using AutoMapper;
using Employees.Database;
using Employees.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employee = Employees.Entities.Employee;

namespace Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        private static readonly IMapper Mapper;

        static EmployeeService()
        {
            Mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<Employee, Database.Employee>();
                config.CreateMap<Database.Employee, Employee>();

            }).CreateMapper();
        }
        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            using var db = new OfficeContext();
            var dbModel = Mapper.Map<Database.Employee>(employee);
            await db.AddAsync(dbModel);
            await db.SaveChangesAsync();
            return dbModel.Id;
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            using var db = new OfficeContext();

            var dbModel = await db.Employees.SingleOrDefaultAsync(x => x.Id == id);
            if (dbModel == null)
                throw new ArgumentException("Employee not found");

            db.Remove(dbModel);
            await db.SaveChangesAsync();
        }

        public async Task EditEmpoyeeAsync(Employee employee)
        {
            using var db = new OfficeContext();

            var dbModel = await db.Employees.SingleOrDefaultAsync(x => x.Id == employee.Id);
            if (dbModel == null)
                throw new ArgumentException("Employee not found");

            Mapper.Map(employee, dbModel);
            await db.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            using var db = new OfficeContext();
            var dbModel = await db.Employees.SingleOrDefaultAsync(x => x.Id == id);
            if (dbModel == null)
                throw new ArgumentException("Employee not found");

            return Mapper.Map<Employee>(dbModel);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            using var db = new OfficeContext();
            var dbModels = await db.Employees.ToListAsync();
            return Mapper.Map<List<Employee>>(dbModels);
        }
    }
}
