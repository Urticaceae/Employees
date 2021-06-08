using AutoMapper;
using Employees.Entities;
using Employees.Interface;
using Employees.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private static IMapper Mapper;
        static EmployeesController()
        {
            Mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<Employee, EmployeeViewModel>();

            }).CreateMapper();
        }

        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IProgrammingLanguageService _programmingLanguageService;

        public EmployeesController(IEmployeeService employeeService,
            IDepartmentService departmentService,
            IProgrammingLanguageService programmingLanguageService,
            ILogger<EmployeesController> logger)
        {
            _logger = logger;
            _employeeService = employeeService;
            _departmentService = departmentService;
            _programmingLanguageService = programmingLanguageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            var departments = (await _departmentService.GetDepartmentsAsync()).ToDictionary(x => x.Id, x => x.Name);
            var languages = (await _programmingLanguageService.GetProgrammingLanguagesAsync()).ToDictionary(x => x.Id, x => x.Name);
            var models = new List<EmployeeViewModel>();
            foreach(var employee in employees)
            {
                var model = Mapper.Map<EmployeeViewModel>(employee);
                model.DepartmentName = departments.TryGetValue(employee.DepartmentId, out var d) ? d : "Unknown";
                model.ProgrammingLanguage = languages.TryGetValue(employee.ProgrammingLanguageId, out var l) ? l : "Unknown";
                models.Add(model);
            }
            return Ok(models);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeViewModel), 200)]
        public async Task<IActionResult> GetEmployeeAsync(int id)
        {
            var employee = await _employeeService.GetEmployeeAsync(id);
            var department = await _departmentService.GetDepartmentAsync(employee.DepartmentId);
            var language = await _programmingLanguageService.GetProgrammingLanguageAsync(employee.ProgrammingLanguageId);
            var model = Mapper.Map<EmployeeViewModel>(employee);
            model.DepartmentName = department.Name;
            model.ProgrammingLanguage = language.Name;
            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmployeeViewModel), 201)]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] Employee employee)
        {
            var id = await _employeeService.AddEmployeeAsync(employee);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditEmployeeAsync([FromBody] Employee employee)
        {
            await _employeeService.EditEmpoyeeAsync(employee);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
