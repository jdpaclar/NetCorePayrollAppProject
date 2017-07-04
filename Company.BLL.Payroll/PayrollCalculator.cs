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

        public bool GetCalculatedEmployeePayroll(ref EmployeePayrollItem pEmployeeData, out string pMessage)
        {
            pMessage = string.Empty;

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

                pEmployeeData.GrossIncome = vGrossIncome;
                pEmployeeData.IncomeTax = vIncomeTax;
                pEmployeeData.NetIncome = vNetIncome;
                pEmployeeData.Super = vSuper;
            }
            catch (ArgumentException arex)
            {
                pMessage = arex.Message;
            }
            catch (Exception ex)
            {
                pMessage = "Failed to Process Payroll";

                // Do App logging
                _appLogger.LogError(ex.Message);
            }

            return false;
        }

    }
}
