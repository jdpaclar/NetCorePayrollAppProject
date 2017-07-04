using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.BLL.Payroll.Interface;

namespace Company.Website.Payroll.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPayrollCalculator _payCalc;

        public HomeController(IPayrollCalculator payCalc)
        {
            _payCalc = payCalc;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Error()
        {
            return View();
        }
    }
}
