using Company.BLL.Payroll.Interface;
using Company.Svc.Payroll;
using System;
using System.Collections.Generic;
using Company.Common.Utilities;
using Company.BLL.Payroll.Extension;
using Microsoft.Extensions.Logging;

namespace Company.BLL.Payroll
{
    public partial class PayrollCalculator: IPayrollCalculator
    {
        private readonly IIncomeTaxFormulaConifg _IncomeTaxFormula;
        private readonly ILogger<PayrollCalculator> _appLogger;

        public PayrollCalculator(IIncomeTaxFormulaConifg pIncomeTaxFormula, ILogger<PayrollCalculator> pAppLogger)
        {
            _IncomeTaxFormula = pIncomeTaxFormula;
            _appLogger = pAppLogger;
        }

        public void CalculateEmployeePayroll(EmployeePayrollItem pEmployeeData, out CalculatedPayrollItem pCalculatedResult)
        {
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
                    Super = vSuper
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
        }

    }
}
