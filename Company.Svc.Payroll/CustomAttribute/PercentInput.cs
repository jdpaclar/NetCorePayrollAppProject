using Company.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Company.Svc.Payroll.CustomAttribute
{
    public class PercentInput: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string pPercentValue = value as string;

            if (!string.IsNullOrWhiteSpace(pPercentValue))
            {
                return pPercentValue.IsValidPercentString();
            }

            return false;
        }
    }
}
