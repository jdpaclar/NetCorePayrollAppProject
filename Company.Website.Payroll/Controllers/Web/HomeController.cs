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
        public IActionResult Index()
        {
            return View();
        }
    }
}
