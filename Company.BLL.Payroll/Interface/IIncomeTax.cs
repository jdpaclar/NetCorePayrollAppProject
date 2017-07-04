using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Payroll.Interface
{
    public interface IIncomeTax
    {
        decimal GetIncomeTax(decimal pAnnualSalary);

        bool EvaluateIfSalaryInTaxRange(decimal pAnnualSalary);
    }
}
