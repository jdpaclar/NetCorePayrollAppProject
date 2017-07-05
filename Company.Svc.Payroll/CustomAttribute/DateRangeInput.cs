using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Company.Common.Utilities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Svc.Payroll.CustomAttribute
{
    public class DateRangeInput: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string strValue = value as string;

            if (!string.IsNullOrWhiteSpace(strValue))
            {
                if (!strValue.IsDateRangeValidFormat())
                    return false;
                else
                    return true;
            }

            return false;
        }
    }
}
