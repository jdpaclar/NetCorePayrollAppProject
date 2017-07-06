using AutoMapper;
using Company.BLL.Payroll.Interface;
using Company.Common.Utilities;
using Company.Svc.Payroll;
using Company.Website.Payroll.Controllers.Api;
using Company.Website.Payroll.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.xUnitTest.Website.Payroll
{
    public class EmployeePayrollWebAPITest
    {
        public EmployeePayrollWebAPITest()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<CalculatedPayrollItem, EmployeePayrollResultVM>()
                .ForMember(emp => emp.FullName, opt => opt.MapFrom(src => "{0} {1}".FormatString(src.FirstName, src.LastName)))
                .ForMember(emp => emp.PayPeriod, opt => opt.MapFrom(src => "{0} – {1}".FormatString(src.StartDate, src.EndDate)));
            });
        }

        [Fact]
        public void TestEmployeePayrollController()
        {
            var empPayrollInput = new EmployeePayrollItem
            {
                FirstName = "John",
                LastName = "Doe",
                DateInput = "01 March - 02 March",
                AnnualSalary = 60050,
                SuperRate = "9%"
            };

            Mock<IPayrollFactory> payrollFact = new Mock<IPayrollFactory>();

            payrollFact.Setup(x => x.CalculatePayroll(empPayrollInput));

            var empController = new EmployeePayrollController(payrollFact.Object, null);

            var postResutl = Assert.IsType<CreatedResult>(empController.Post(empPayrollInput));
        }
    }
}
