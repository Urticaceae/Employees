using Employees.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Employees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProgrammingLanguageController : ControllerBase
    {
        private readonly IProgrammingLanguageService _programmingLanguageService;

        public ProgrammingLanguageController(IProgrammingLanguageService programmingLanguageService)
        {
            _programmingLanguageService = programmingLanguageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLanguagesAsync()
        {
            var result = await _programmingLanguageService.GetProgrammingLanguagesAsync();
            return Ok(result);
        }


        [HttpPost("language")]
        public async Task<IActionResult> CreateLanguageAsync([FromBody] string language)
        {
            await _programmingLanguageService.AddLanguageAsync(language);
            return Ok();
        }
    }
}
