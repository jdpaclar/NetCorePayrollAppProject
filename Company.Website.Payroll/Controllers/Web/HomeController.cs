using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.BLL.Payroll.Interface;
using Microsoft.AspNetCore.Http;
using Company.Svc.Payroll;
using System.IO;
using Company.Svc.Payroll.Extensions;
using AutoMapper;
using Company.Website.Payroll.ViewModel;
using System.Text;

namespace Company.Website.Payroll.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPayrollFactory _payFactory;

        public HomeController(IPayrollFactory payFactory)
        {
            _payFactory = payFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CSVProcess(IFormFile files)
        {
            string vCSVLine;

            var payrollInputItems = new List<EmployeePayrollItem>();

            IEnumerable<EmployeePayrollResultVM> pResults = null;
            
            if (files.Length > 0)
            {
                using (var csvReader = new StreamReader(files.OpenReadStream()))
                {
                    while ((vCSVLine = csvReader.ReadLine()) != null)
                    {
                        payrollInputItems.Add(vCSVLine.ToEmployeePayrollItems());
                    }
                }

                var calculatedResults = _payFactory.CalculatePayrollList(payrollInputItems);

                pResults = Mapper.Map<IEnumerable<EmployeePayrollResultVM>>(calculatedResults);
            }

            return File(new UTF8Encoding().GetBytes(pResults.ToCSVStringFormat()), "text/csv", "Output.csv");
        }
    }
}
