using Employees.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Interface
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetDepartmentsAsync();

        public Task<Department> GetDepartmentAsync(int id);

        public Task<int> AddDepartmentAsync(Department department);

        public Task DeleteDepartmentAsync(int id);
    }
}
