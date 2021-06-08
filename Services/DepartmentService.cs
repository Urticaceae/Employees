using AutoMapper;
using Employees.Database;
using Employees.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Department = Employees.Entities.Department;

namespace Employees.Services
{
    public class DepartmentService : IDepartmentService
    {

        private static readonly IMapper Mapper;

        static DepartmentService()
        {
            Mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<Department, Database.Department>();
                config.CreateMap<Database.Department, Department>();

            }).CreateMapper();
        }

        public async Task<int> AddDepartmentAsync(Department department)
        {
            using var db = new OfficeContext();
            var dbModel = Mapper.Map<Database.Department>(department);
            await db.AddAsync(dbModel);
            await db.SaveChangesAsync();
            return dbModel.Id;
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            using var db = new OfficeContext();
            var dbModel = await db.Departments.SingleOrDefaultAsync(x => x.Id == id);
            if (dbModel == null)
                throw new ArgumentException("Unknown department");

            db.Remove(dbModel);
            await db.SaveChangesAsync();
        }

        public async Task<Department> GetDepartmentAsync(int id)
        {
            using var db = new OfficeContext();
            var dbModel = await db.Departments.SingleOrDefaultAsync(x => x.Id == id);
            if (dbModel == null)
                throw new ArgumentException("Unknown department");

            return Mapper.Map<Department>(dbModel);
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            using var db = new OfficeContext();
            var dbModels = await db.Departments.ToListAsync();
            return Mapper.Map<List<Department>>(dbModels);
        }
    }
}
