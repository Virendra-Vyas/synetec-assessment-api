using SynetecAssessmentApi.Dtos;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetEmployeeAsync(int selectedEmployeeId);
    }
}
