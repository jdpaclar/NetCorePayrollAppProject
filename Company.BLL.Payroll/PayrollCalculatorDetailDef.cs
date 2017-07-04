using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Payroll
{
    // Detailed Implementations
    public partial class PayrollCalculator
    {
        public decimal GetEmployeeIncomeTax(decimal pAnnualSalary)
        {
            var lstFormula = _IncomeTaxFormula.GetIncomeTaxConfiguration();

            if (lstFormula.Count() == 0)
                throw new ArgumentException("No Formula Setup.");

            var lIncomeTax = lstFormula.Where(tax => tax.EvaluateIfSalaryInTaxRange(pAnnualSalary)).SingleOrDefault();

            if (lIncomeTax == null)
                throw new ArgumentException("Salary Range not Handled.");

            return lIncomeTax.GetIncomeTax(pAnnualSalary);
        }

        public decimal GetGrossIncome(decimal pAnnualSalary)
        {
            return pAnnualSalary / 12;
        }

        public decimal GetNetIncome(decimal pAnnualSalary)
        {
            decimal vGrossIncome = GetGrossIncome(pAnnualSalary);
            decimal vIncomeTax = GetEmployeeIncomeTax(pAnnualSalary);

            return vGrossIncome - vIncomeTax;
        }

        public decimal GetSuper(decimal pAnnualSalary, decimal pSuperRate)
        {
            decimal vGrossIncome = GetGrossIncome(pAnnualSalary);
            return vGrossIncome * pSuperRate;
        }
    }
}
