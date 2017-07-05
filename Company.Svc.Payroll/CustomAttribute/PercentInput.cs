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
                if (!pPercentValue.Contains("%"))
                    return false;   

                Regex reg = new Regex(@"^(\d+|\d+[.]\d+)%?$");
                if (!reg.IsMatch(pPercentValue))
                    return false;
                else
                    return true;
            }

            return false;
        }
    }
}
