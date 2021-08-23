using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Persistence.Interfaces;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;

        public EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }
        public async Task<EmployeeDto> GetEmployeeAsync(int selectedEmployeeId)
        {
            var match = await _repository.FindSingleAsync(x => x.Id == selectedEmployeeId);

            if (match == null)
            {
                return null;
            }

            return new EmployeeDto
            {
                Fullname = match.Fullname,
                JobTitle = match.JobTitle
            };
        }
    }
}
