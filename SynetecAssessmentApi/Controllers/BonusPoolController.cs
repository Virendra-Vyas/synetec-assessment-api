using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolService _bonusPoolService;

        public BonusPoolController(IBonusPoolService bonusPoolService)
        {
            _bonusPoolService = bonusPoolService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bonusPoolService.GetEmployeesAsync());
        }

        [HttpGet]
        [Route("calculateBonus")]
        public async Task<IActionResult> CalculateBonus([FromQuery] CalculateBonusDto request)
        {
            return Ok(await _bonusPoolService.CalculateAsync(
              request.TotalBonusPoolAmount,
              request.SelectedEmployeeId));
        }
    }
}
