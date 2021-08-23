using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeAsync(EmployeeIdQuery employeeIdQuery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid Employee Id" });
            }

            var match = await _employeeService.GetEmployeeAsync(employeeIdQuery.SelectedEmployeeId);

            if (match == null)
            {
                return BadRequest(new { message = "Employee not found" });
            }

            return Ok(match);
        }

    }
}
