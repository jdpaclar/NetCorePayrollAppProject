using AutoMapper;
using Company.BLL.Payroll.Interface;
using Company.Svc.Payroll;
using Company.Svc.Payroll.Extensions;
using Company.Website.Payroll.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Website.Payroll.Extensions;

namespace Company.Website.Payroll.Controllers.Web
{
    public class EmployeeController: Controller
    {
        private readonly IPayrollFactory _payFactory;

        public EmployeeController(IPayrollFactory payFactory)
        {
            _payFactory = payFactory;
        }

        [HttpPost]
        public JsonResult CalculateEmployeePayroll([FromBody]EmployeePayrollItem pEmpPayroll)
        {
            var response = new CalculateEmployeePayrollResponse();

            if (!ModelState.IsValid)
            {
                var err = ModelState.Values.SelectMany(v => v.Errors);
                return Json(response.ToFailedResponseBase(err));
            }

            var result = _payFactory.CalculatePayroll(pEmpPayroll);
            response.IsSuccess = true;
            response.CalculatedPayroll = Mapper.Map<EmployeePayrollResultVM>(result);

            return Json(response);
        }

        [HttpPost]
        public IActionResult CSVProcess(IFormFile files)
        {
            try
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
            catch (ArgumentException)
            {
                return RedirectToAction("Error");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
