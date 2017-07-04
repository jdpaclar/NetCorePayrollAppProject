using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Payroll.Extension
{
    public static class DecimalRoundingExtension
    {
        /// <summary>
        /// Change this Extension if rule on rounding change
        /// </summary>
        /// <param name="pRoundEval"></param>
        /// <returns></returns>
        public static decimal ToRoundedValue(this decimal pRoundEval)
        {
            decimal vDecimalValue = pRoundEval - Math.Truncate(pRoundEval);

            if (vDecimalValue >= 0.50m)
                return Math.Ceiling(pRoundEval);
            else
                return Math.Floor(pRoundEval);
        }
    }
}
