using AutoMapper;
using Company.BLL.Payroll.Interface;
using Company.Svc.Payroll;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Website.Payroll.ViewModel;

namespace Company.Website.Payroll.Controllers.Api
{
    [Route("/api/payrollcalculate/")]
    public class EmployeePayrollController: Controller
    {
        private readonly IPayrollFactory _payrollcalc;
        private readonly ILogger<EmployeePayrollController> _logger;

        public EmployeePayrollController(IPayrollFactory pPayrollCalc, ILogger<EmployeePayrollController> pLogger)
        {
            _payrollcalc = pPayrollCalc;
            _logger = pLogger;
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]EmployeePayrollItem employeeRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                var result = _payrollcalc.CalculatePayroll(employeeRequest);

                var employeeOutResult = Mapper.Map<EmployeePayrollResultVM>(result);

                return Ok(employeeOutResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Service {0}", ex);
            }

            return BadRequest("Failed to Get Payroll Info.");
        }
    }
}
