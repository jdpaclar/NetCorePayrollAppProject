using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Payroll.Interface;

namespace Company.BLL.Payroll
{
    public class IncomeTaxFormulaConfig : IIncomeTaxFormulaConifg
    {
        public List<IIncomeTax> GetIncomeTaxConfiguration()
        {
            return new List<IIncomeTax>
            {
                new Level1IncomeTax(),
                new Level2IncomeTax(),
                new Level3IncomeTax(),
                new Level4IncomeTax(),
                new Level5IncomeTax()
            };
        }
    }
}
