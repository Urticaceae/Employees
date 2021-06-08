using Employees.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Interface
{
    public interface IProgrammingLanguageService
    {
        public Task<List<ProgrammingLanguage>> GetProgrammingLanguagesAsync();

        public Task<ProgrammingLanguage> GetProgrammingLanguageAsync(int id);

        public Task<int> AddLanguageAsync(string language);

        public Task DeleteLanguageAsync(int id);
    }
}
