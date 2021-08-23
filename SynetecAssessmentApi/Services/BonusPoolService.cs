using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly IRepository<Employee> _repository;

        public BonusPoolService(IRepository<Employee> employeeRepository)
        {
            _repository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _repository.AllIncludingAsync(e => e.Department);


            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                result.Add(
                    new EmployeeDto
                    {
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    });
            }

            return result;
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {

            //load the details of the selected employee using the Id
            Employee employee = await _repository.GetSingleAsync(x => x.Id == selectedEmployeeId, (x => x.Department));

            if (employee == null)
            {
                return new BonusPoolCalculatorResultDto();
            }

            var employees = await _repository.GetAllAsync();

            //get the total salary budget for the company
            int totalSalary = employees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
            int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);

            return new BonusPoolCalculatorResultDto
            {
                Employee = new EmployeeDto
                {
                    Fullname = employee.Fullname,
                    JobTitle = employee.JobTitle,
                    Salary = employee.Salary,
                    Department = new DepartmentDto
                    {
                        Title = employee.Department.Title,
                        Description = employee.Department.Description
                    }
                },

                Amount = bonusAllocation
            };
        }
    }
}
