using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Svc.Payroll
{
    public class EmployeePayrollItem
    {
        [MaxLength(250)]
        public string FirstName { get; set; }

        [MaxLength(250)]
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public string SuperRate { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
