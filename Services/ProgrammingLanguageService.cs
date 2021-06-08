using AutoMapper;
using Employees.Database;
using Employees.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammingLanguage = Employees.Entities.ProgrammingLanguage;

namespace Employees.Services
{
    public class ProgrammingLanguageService : IProgrammingLanguageService
    {
        private static readonly IMapper Mapper;

        static ProgrammingLanguageService()
        {
            Mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<ProgrammingLanguage, Database.ProgrammingLanguage>();
                config.CreateMap<Database.ProgrammingLanguage, ProgrammingLanguage>();

            }).CreateMapper();
        }

        public async Task<int> AddLanguageAsync(string language)
        {
            using var db = new OfficeContext();
            var dbModel = new Database.ProgrammingLanguage { Name = language };
            await db.AddAsync(dbModel);
            await db.SaveChangesAsync();
            return dbModel.Id;
        }

        public async Task DeleteLanguageAsync(int id)
        {
            using var db = new OfficeContext();
            var dbModel = await db.ProgrammingLanguages.SingleOrDefaultAsync(x => x.Id == id);
            if (dbModel == null)
                throw new ArgumentException("Unknown language");

            db.Remove(dbModel);
            await db.SaveChangesAsync();
        }

        public async Task<ProgrammingLanguage> GetProgrammingLanguageAsync(int id)
        {
            using var db = new OfficeContext();
            var dbModel = await db.ProgrammingLanguages.SingleOrDefaultAsync(x => x.Id == id);
            if (dbModel == null)
                throw new ArgumentException("Unknown language");

            return Mapper.Map<ProgrammingLanguage>(dbModel);
        }

        public async Task<List<ProgrammingLanguage>> GetProgrammingLanguagesAsync()
        {
            using var db = new OfficeContext();
            var dbModels = await db.ProgrammingLanguages.ToListAsync();
            return Mapper.Map<List<ProgrammingLanguage>>(dbModels);
        }

    }
}
