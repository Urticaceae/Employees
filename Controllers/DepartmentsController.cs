using Employees.Entities;
using Employees.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartmentAsync([FromBody] Department department)
        {
            await _departmentService.AddDepartmentAsync(department);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentsAsync()
        {
            var result = await _departmentService.GetDepartmentsAsync();
            return Ok(result);
        }
    }
}
