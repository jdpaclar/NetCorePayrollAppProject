using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Common.Utilities;

namespace Company.Website.Payroll.ViewModel
{
    public static class EmployeePayrollResultVMsExtensions
    {
        public static string ToCSVStringFormat(this IEnumerable<EmployeePayrollResultVM> pEmployeePayrollVM)
        {
            if(pEmployeePayrollVM is null)
                return "";

            StringBuilder sb = new StringBuilder();

            foreach (var payroll in pEmployeePayrollVM)
            {
                sb.Append("{0},{1},{2},{3},{4},{5},{6}".FormatString( 
                    payroll.FullName,
                    payroll.PayPeriod,
                    payroll.GrossIncome,
                    payroll.IncomeTax, 
                    payroll.NetIncome, 
                    payroll.Super,
                    Environment.NewLine));
            }

            return sb.ToString();
        }
    }
}
