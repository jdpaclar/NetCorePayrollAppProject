using Company.BLL.Payroll;
using Company.BLL.Payroll.Extension;
using Company.BLL.Payroll.Interface;
using Company.Common.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Test.PayrollBll
{
    [TestClass]
    public class PayrollCalculatorTest
    {
        [TestMethod]
        public void TestCorrectEmployeeIncomeTax()
        {
            decimal vAnnualSalary = 60050m;
            
            Mock<IIncomeTaxFormulaConifg> formulaConf = new Mock<IIncomeTaxFormulaConifg>();
            formulaConf.Setup(x => x.GetIncomeTaxConfiguration()).Returns(new List<IIncomeTax>
            {
                new Level1IncomeTax(),
                new Level2IncomeTax(),
                new Level3IncomeTax(),
                new Level4IncomeTax(),
                new Level5IncomeTax()
            });

            var payCalculator = new PayrollCalculator(formulaConf.Object, null);

            Assert.AreEqual(922, payCalculator.GetEmployeeIncomeTax(vAnnualSalary).ToRoundedValue());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "")]
        public void TestSalaryNotInRangeForIncomeTax()
        {
            decimal vAnnualSalary = 20000m;

            Mock<IIncomeTaxFormulaConifg> formulaConf = new Mock<IIncomeTaxFormulaConifg>();
            formulaConf.Setup(x => x.GetIncomeTaxConfiguration()).Returns(new List<IIncomeTax>
            {
                new Level1IncomeTax(),
                new Level5IncomeTax()
            });

            var payCal = new PayrollCalculator(formulaConf.Object, null);

            payCal.GetEmployeeIncomeTax(vAnnualSalary);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "")]
        public void TestIfNoTaxFormula()
        {
            decimal vAnnualSalary = 60050m;

            Mock<IIncomeTaxFormulaConifg> formulaConf = new Mock<IIncomeTaxFormulaConifg>();
            formulaConf.Setup(x => x.GetIncomeTaxConfiguration()).Returns(new List<IIncomeTax>());

            var payCal = new PayrollCalculator(formulaConf.Object, null);

            payCal.GetEmployeeIncomeTax(vAnnualSalary);
        }

        [TestMethod]
        public void TestGrossIncome()
        {
            decimal vAnnualSalary = 60050m;

            Mock<IIncomeTaxFormulaConifg> formulaConf = new Mock<IIncomeTaxFormulaConifg>();
            formulaConf.Setup(x => x.GetIncomeTaxConfiguration()).Returns(new List<IIncomeTax>());

            var payCal = new PayrollCalculator(formulaConf.Object, null);

            Assert.AreEqual(5004, payCal.GetGrossIncome(vAnnualSalary).ToRoundedValue());
        }

        [TestMethod]
        public void TestNetIncome()
        {
            decimal vAnnualSalary = 60050m;

            Mock<IIncomeTaxFormulaConifg> formulaConf = new Mock<IIncomeTaxFormulaConifg>();
            formulaConf.Setup(x => x.GetIncomeTaxConfiguration()).Returns(new List<IIncomeTax>
            {
                new Level1IncomeTax(),
                new Level2IncomeTax(),
                new Level3IncomeTax(),
                new Level4IncomeTax(),
                new Level5IncomeTax()
            });
            var payCalculator = new PayrollCalculator(formulaConf.Object, null);

            Assert.AreEqual(4082, payCalculator.GetNetIncome(vAnnualSalary).ToRoundedValue());
        }

        [TestMethod]
        public void TestSuper()
        {
            decimal vAnnualSalary = 60050m;
            string vPercentage = "9%";

            Mock<IIncomeTaxFormulaConifg> formulaConf = new Mock<IIncomeTaxFormulaConifg>();
            formulaConf.Setup(x => x.GetIncomeTaxConfiguration()).Returns(new List<IIncomeTax>());

            var payCalculator = new PayrollCalculator(formulaConf.Object, null);

            Assert.AreEqual(450, payCalculator.GetSuper(vAnnualSalary, vPercentage.ToDecimalFromPercentage()).ToRoundedValue());
        }
    }
}
