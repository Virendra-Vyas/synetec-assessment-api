using SynetecAssessmentApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public interface IBonusPoolService
    {
        public Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        public Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId);

    }
}
