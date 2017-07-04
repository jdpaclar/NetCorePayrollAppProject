using Company.BLL.Payroll;
using Company.BLL.Payroll.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Test.PayrollBll
{
    [TestClass]
    public class IncomeTaxTest
    {
        #region Calculate Test
        [TestMethod]
        public void TestLevel1IncomeTaxCalculate()
        {
            var lvlIncomeTax = new Level1IncomeTax();
            Assert.AreEqual(0, lvlIncomeTax.GetIncomeTax(17000m));
        }

        [TestMethod]
        public void TestLevel2IncomeTaxCalculate()
        {
            var lvlIncomeTax = new Level2IncomeTax();
            decimal vAnnualSalary = 18500m;
            decimal vCalculatedIncomeTaxRounded = lvlIncomeTax.GetIncomeTax(vAnnualSalary).ToRoundedValue();

            Assert.AreEqual(5m, vCalculatedIncomeTaxRounded);
        }

        [TestMethod]
        public void TestLevel3IncomeTaxCalculate()
        {
            var lvlIncomeTax = new Level3IncomeTax();
            decimal vAnnualSalary = 60050m;
            decimal vCalculatedIncomeTaxRounded = lvlIncomeTax.GetIncomeTax(vAnnualSalary).ToRoundedValue();
            Assert.AreEqual(922m, vCalculatedIncomeTaxRounded);
        }

        [TestMethod]
        public void TestLevel4IncomeTaxCalculate()
        {
            var lvlIncomeTax = new Level4IncomeTax();
            decimal vAnnualSalary = 80500;
            decimal vCalculatedIncomeTaxRounded = lvlIncomeTax.GetIncomeTax(vAnnualSalary).ToRoundedValue();
            Assert.AreEqual(1478m, vCalculatedIncomeTaxRounded);
        }

        [TestMethod]
        public void TestLevel5IncomeTaxCalculate()
        {
            var lvlIncomeTax = new Level5IncomeTax();
            decimal vAnnualSalary = 189000m;
            decimal vCalculatedIncomeTaxRounded = lvlIncomeTax.GetIncomeTax(vAnnualSalary).ToRoundedValue();
            Assert.AreEqual(4883m, vCalculatedIncomeTaxRounded);
        }
        #endregion

        #region Validate Min Max Salaries Exceptions
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "")]
        public void ValidateLevel1MaxIncomeLevel()
        {
            var lvl1IncomeTax = new Level1IncomeTax();
            lvl1IncomeTax.GetIncomeTax(18201);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "")]
        public void ValidateLevel2MinIncomeLevel()
        {
            var lvl2IncomeTax = new Level2IncomeTax();
            lvl2IncomeTax.GetIncomeTax(18200m);
        }
        #endregion
    }
}
