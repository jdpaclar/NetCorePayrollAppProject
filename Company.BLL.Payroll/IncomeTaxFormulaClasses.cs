using Company.BLL.Payroll.Interface;
using System;
using Company.Common.Utilities;

namespace Company.BLL.Payroll
{
    public abstract class IncomeTaxBase : IIncomeTax
    {
        protected decimal _maxSalary;
        protected decimal _minSalary = 0;

        public virtual decimal GetIncomeTax(decimal pAnnualSalary)
        {
            if (pAnnualSalary > _maxSalary)
                throw new ArgumentException("Income is Greater than ${0}".FormatString(_maxSalary));

            if (pAnnualSalary < _minSalary)
                throw new ArgumentException("Income is Lesser than ${0}".FormatString(_minSalary));

            return pAnnualSalary;
        }

        public bool EvaluateIfSalaryInTaxRange(decimal pAnnualSalary)
        {
            if (pAnnualSalary >= _minSalary && pAnnualSalary <= _maxSalary)
                return true;
            else
                return false;
        }
    }

    public class Level1IncomeTax : IncomeTaxBase
    {
        public Level1IncomeTax()
        {
            _maxSalary = 18200m;
        }

        public override decimal GetIncomeTax(decimal pAnnualSalary)
        {
            decimal vSalaryValidate = base.GetIncomeTax(pAnnualSalary);
            return 0;
        }
    }

    public class Level2IncomeTax : IncomeTaxBase
    {
        public Level2IncomeTax()
        {
            _maxSalary = 37000m;
            _minSalary = 18201m;
        }

        public override decimal GetIncomeTax(decimal pAnnualSalary)
        {
            decimal vAnnualValidated = base.GetIncomeTax(pAnnualSalary);
            return ((vAnnualValidated - 18200) * 0.19m) / 12;
        }
    }

    public class Level3IncomeTax : IncomeTaxBase
    {
        public Level3IncomeTax()
        {
            _maxSalary = 80000m;
            _minSalary = 37001m;
        }

        public override decimal GetIncomeTax(decimal pAnnualSalary)
        {
            decimal vAnnualValidated = base.GetIncomeTax(pAnnualSalary);
            return (3572m + (0.325m * (vAnnualValidated - 37000))) / 12;
        }
    }

    public class Level4IncomeTax : IncomeTaxBase
    {
        public Level4IncomeTax()
        {
            _maxSalary = 180000m;
            _minSalary = 80001m;
        }

        public override decimal GetIncomeTax(decimal pAnnualSalary)
        {
            decimal vAnnualValidated = base.GetIncomeTax(pAnnualSalary);
            return (17547m + (0.37m * (vAnnualValidated - 80000))) / 12;
        }
    }

    public class Level5IncomeTax : IncomeTaxBase
    {
        public Level5IncomeTax()
        {
            _minSalary = 180001m;
        }

        public override decimal GetIncomeTax(decimal pAnnualSalary)
        {
            if (pAnnualSalary < _minSalary)
                throw new ArgumentException("Income is Lesser than {0}".FormatString(_maxSalary));

            return (54547m + (0.45m * (pAnnualSalary - 180000))) / 12;
        }
    }
}
