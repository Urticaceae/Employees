using Employees.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees.Interface
{
    public interface IEmployeeService
    {
        public Task<int> AddEmployeeAsync(Employee employee);

        public Task EditEmpoyeeAsync(Employee employee);

        public Task<List<Employee>> GetEmployeesAsync();

        public Task<Employee> GetEmployeeAsync(int id);

        public Task DeleteEmployeeAsync(int id);
    }
}
