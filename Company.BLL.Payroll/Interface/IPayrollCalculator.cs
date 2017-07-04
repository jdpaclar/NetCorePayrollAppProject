﻿using Company.Svc.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Payroll.Interface
{
    public interface IPayrollCalculator
    {
        void CalculateEmployeePayroll(EmployeePayrollItem pEmployeeData, out CalculatedPayrollItem pCalculatedResult);
    }
}
