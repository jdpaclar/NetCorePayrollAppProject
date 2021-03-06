﻿using Company.Svc.Payroll.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Svc.Payroll
{
    public class CalculatedPayrollItem: IResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetIncome { get; set; }
        public decimal Super { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
