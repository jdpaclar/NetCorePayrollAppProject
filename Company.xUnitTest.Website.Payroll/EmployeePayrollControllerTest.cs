using AutoMapper;
using Company.BLL.Payroll.Interface;
using Company.Common.Utilities;
using Company.Svc.Payroll;
using Company.Svc.Payroll.Extensions;
using Company.Website.Payroll.Controllers.Web;
using Company.Website.Payroll.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.xUnitTest.Website.Payroll
{
    public class EmployeePayrollControllerTest
    {
        public EmployeePayrollControllerTest()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<CalculatedPayrollItem, EmployeePayrollResultVM>()
                .ForMember(emp => emp.FullName, opt => opt.MapFrom(src => "{0} {1}".FormatString(src.FirstName, src.LastName)))
                .ForMember(emp => emp.PayPeriod, opt => opt.MapFrom(src => "{0} – {1}".FormatString(src.StartDate, src.EndDate)));
            });
        }

        [Fact]
        public void TestIfActionReturnsCSVFile()
        {
            var fileMock = new Mock<IFormFile>();

            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            string csvLine = "David,Rudd,60050,9%,01 March – 31 March";

            writer.Write(csvLine);
            writer.Flush();

            ms.Position = 0;

            fileMock.Setup(x => x.FileName).Returns("Input.csv");
            fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            fileMock.Setup(x => x.Length).Returns(ms.Length);

            Mock<IPayrollFactory> payrollFactory = new Mock<IPayrollFactory>();

            payrollFactory.Setup(x => x.CalculatePayrollList(new List<EmployeePayrollItem>
            {
                csvLine.ToEmployeePayrollItems()
            }));

            var homeController = new EmployeeController(payrollFactory.Object);

            var result = homeController.CSVProcess(fileMock.Object);

            var viewResult = Assert.IsType<FileContentResult>(result);

            Assert.Equal(viewResult.ContentType, "text/csv");
        }

        [Fact]
        public void CSVUploadIsProvidedWithWrongFormat()
        {
            var fileMock = new Mock<IFormFile>();

            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            string csvLine = "David,Rudd,asdasd,asdasd,01 March – 31 March";

            writer.Write(csvLine);
            writer.Flush();

            ms.Position = 0;

            fileMock.Setup(x => x.FileName).Returns("Input.csv");
            fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            fileMock.Setup(x => x.Length).Returns(ms.Length);

            Mock<IPayrollFactory> payrollFactory = new Mock<IPayrollFactory>();

            payrollFactory.Setup(x => x.CalculatePayrollList(new List<EmployeePayrollItem>
            {
                csvLine.ToEmployeePayrollItems()
            }));

            var empController = new EmployeeController(payrollFactory.Object);

            var result = empController.CSVProcess(fileMock.Object);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Error", viewResult.ControllerName);
        }

        [Fact]
        public void CalclatePayrollActionReturnsIsSuccessFalse()
        {
            var employeePayrollItem = new EmployeePayrollItem
            {
                FirstName = "",
                SuperRate = "asdasdasd" // Invalid Format
            };

            Mock<IPayrollFactory> payrollFactory = new Mock<IPayrollFactory>();

            payrollFactory.Setup(x => x.CalculatePayroll(employeePayrollItem));

            var empController = new EmployeeController(payrollFactory.Object);

            var viewRes = Assert.IsType<JsonResult>(empController.CalculateEmployeePayroll(employeePayrollItem));

            //Assert.False(viewRes.)
        }
    }
}
