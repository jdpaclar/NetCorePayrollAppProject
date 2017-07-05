using Company.Svc.Payroll;
using Company.Svc.Payroll.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Payroll.Interface
{
    public interface IPayrollFactory
    {
        IEnumerable<IResult> CalculatePayrollList(IEnumerable<IInput> pPayrollDataList);

        IResult CalculatePayroll(IInput pPayrollData);
    }
}
