using Company.BLL.Payroll.Extension;
using Company.BLL.Payroll.Interface;
using Company.Common.Utilities;
using Company.Svc.Payroll;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Payroll
{
    public class EmployeePayrollCalculator: IEmployeePayrollCalculator
    {
        private readonly IIncomeTaxFormulaConifg _IncomeTaxFormula;
        private readonly ILogger<EmployeePayrollCalculator> _appLogger;

        public EmployeePayrollCalculator(IIncomeTaxFormulaConifg pIncomeTaxFormula, ILogger<EmployeePayrollCalculator> pAppLogger)
        {
            _IncomeTaxFormula = pIncomeTaxFormula;
            _appLogger = pAppLogger;
        }

        public CalculatedPayrollItem CalculateEmployeePayroll(EmployeePayrollItem pEmployeeData)
        {
            CalculatedPayrollItem pCalculatedResult = null;

            decimal vAnnualSalary = pEmployeeData.AnnualSalary;
            decimal vGrossIncome;
            decimal vIncomeTax;
            decimal vNetIncome;
            decimal vSuper;

            try
            {
                // Get Gross Income
                vGrossIncome = GetGrossIncome(vAnnualSalary).ToRoundedValue();

                // Get Income Tax
                vIncomeTax = GetEmployeeIncomeTax(vAnnualSalary).ToRoundedValue(); ;

                // Get Net Income
                vNetIncome = GetNetIncome(vAnnualSalary).ToRoundedValue(); ;

                // Get Super
                vSuper = GetSuper(vAnnualSalary, pEmployeeData.SuperRate.ToDecimalFromPercentage()).ToRoundedValue();

                pCalculatedResult = new CalculatedPayrollItem
                {
                    FirstName = pEmployeeData.FirstName,
                    LastName = pEmployeeData.LastName,
                    GrossIncome = vGrossIncome,
                    IncomeTax = vIncomeTax,
                    NetIncome = vNetIncome,
                    Super = vSuper,
                    AnnualIncome = vAnnualSalary,
                    StartDate = pEmployeeData.DateInput.ToParsedDateStringStartDate(),
                    EndDate = pEmployeeData.DateInput.ToParsedDateStringEndDate()
                };
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Do App logging
                _appLogger.LogError(ex.Message);
                throw;
            }

            return pCalculatedResult;
        }

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
