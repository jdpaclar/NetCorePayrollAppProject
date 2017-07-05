using Company.BLL.Payroll.Interface;
using Company.Svc.Payroll;
using System;
using System.Collections.Generic;
using Company.Common.Utilities;
using Company.BLL.Payroll.Extension;
using Microsoft.Extensions.Logging;
using Company.Svc.Payroll.Interface;

namespace Company.BLL.Payroll
{
    public partial class PayrollFactory: IPayrollFactory
    {
        private readonly IEmployeePayrollCalculator _empPayrollCalc;

        public PayrollFactory(IEmployeePayrollCalculator pEmpPayrollCalc)
        {
            _empPayrollCalc = pEmpPayrollCalc;
        }
        
        public IEnumerable<IResult> CalculatePayrollList(IEnumerable<IInput> pPayrollDataList)
        {
            var results = new List<IResult>();

            foreach (var input in pPayrollDataList)
            {
                var result = CalculatePayroll(input);
                results.Add(result);
            }

            return results;
        }

        public IResult CalculatePayroll(IInput pPayrollData)
        {
            switch (pPayrollData.GetType().Name)
            {
                case "EmployeePayrollItem":
                    return _empPayrollCalc.CalculateEmployeePayroll(pPayrollData as EmployeePayrollItem);
                // You may implement special Payroll or whatever here
                default:
                    throw new InvalidOperationException("Payroll Calculation have not been Implemented. View documentation.");
            }
        }
    }
}
