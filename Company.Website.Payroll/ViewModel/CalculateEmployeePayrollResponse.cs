using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Website.Payroll.ViewModel
{
    public class CalculateEmployeePayrollResponse: ResponseBase
    {
        public EmployeePayrollResultVM CalculatedPayroll { get; set; }
    }
}
