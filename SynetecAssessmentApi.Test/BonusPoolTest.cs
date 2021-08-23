using Moq;
using NUnit.Framework;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence.Interfaces;
using SynetecAssessmentApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Test
{
    public class BonusPoolTest
    {
        private Mock<IRepository<Employee>> MockEmployee;

        [SetUp]
        public void Setup()
        {
            MockEmployee = new Mock<IRepository<Employee>>();
        }

        [Test]
        public async Task ShouldReturnExpectedAmountWhenMatchisFound()
        {
            var expected = 40;

            var employee = new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1)
            {
                Department = new Department(1, "Finance", "The finance department for the company")
            };

            MockEmployee.Setup(x => x.GetSingleAsync(x => x.Id == 1, (x => x.Department))).ReturnsAsync(employee);
            MockEmployee.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Employee>
            {
                new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1),
                new Employee(2, "Janet Jones", "HR Director", 90000, 2),
                new Employee(3, "Robert Rinser", "IT Director", 95000, 3),
                new Employee(4, "Jilly Thornton", "Marketing Manager (Senior)", 55000, 4)
            });

            var test = new BonusPoolService(MockEmployee.Object);
            var actual = await test.CalculateAsync(200, 1);

            Assert.AreEqual(expected, actual.Amount);
        }

        [Test]
        public async Task ShouldReturnZeroAmountWhenThereisNoMatchFound()
        {
            var expected = 0;

            var employee = new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1)
            {
                Department = new Department(1, "Finance", "The finance department for the company")
            };

            MockEmployee.Setup(x => x.GetSingleAsync(x => x.Id == 2, (x => x.Department))).ReturnsAsync(employee);
            MockEmployee.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Employee>
            {
                new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1),
                new Employee(2, "Janet Jones", "HR Director", 90000, 2),
                new Employee(3, "Robert Rinser", "IT Director", 95000, 3),
                new Employee(4, "Jilly Thornton", "Marketing Manager (Senior)", 55000, 4)
            });

            var test = new BonusPoolService(MockEmployee.Object);
            var actual = await test.CalculateAsync(200, 1);

            Assert.AreEqual(expected, actual.Amount);
        }

        [Test]
        public void ShouldThrowDivideByZeroExceptionWhenBothSalaryandTotalSalaryisZero()
        {
            var employee = new Employee(1, "John Smith", "Accountant (Senior)", 0, 1)
            {
                Department = new Department(1, "Finance", "The finance department for the company")
            };

            MockEmployee.Setup(x => x.GetSingleAsync(x => x.Id == 1, (x => x.Department))).ReturnsAsync(employee);
            MockEmployee.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Employee>
            {
                new Employee(1, "John Smith", "Accountant (Senior)", 0,1),
                new Employee(2, "Janet Jones", "HR Director", 0,2)
            });

            var test = new BonusPoolService(MockEmployee.Object);
            Assert.ThrowsAsync<DivideByZeroException>(async () => await test.CalculateAsync(200, 1));
        }
    }
}
