using System.ComponentModel.DataAnnotations;

namespace SynetecAssessmentApi.Dtos
{
    public class EmployeeDto
    {
        public string Fullname { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public DepartmentDto Department { get; set; }
    }

    public class EmployeeIdQuery
    {
        [Required]
        public int SelectedEmployeeId { get; set; }
    }
}
