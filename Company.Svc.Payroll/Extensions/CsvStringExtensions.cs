using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Svc.Payroll.Extensions
{
    public static class CsvStringExtensions
    {
        public static EmployeePayrollItem ToEmployeePayrollItems(this string pLine)
        {
            EmployeePayrollItem pPayrollLine;

            try
            {
                var vLineData = pLine.Split(',');

                pPayrollLine = new EmployeePayrollItem
                {
                    FirstName = vLineData[0],
                    LastName = vLineData[1],
                    AnnualSalary = decimal.Parse(vLineData[2]),
                    SuperRate = vLineData[3],
                    DateInput = vLineData[4]
                };
            }
            catch (Exception)
            {
                throw new ArgumentException("CSV Line Format Invalid!.");
            }

            return pPayrollLine;
        }
    }
}
