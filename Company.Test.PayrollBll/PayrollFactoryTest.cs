using Company.BLL.Payroll;
using Company.BLL.Payroll.Interface;
using Company.Svc.Payroll;
using Company.Svc.Payroll.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Test.PayrollBll
{
    [TestClass]
    public class PayrollFactoryTest
    {
        [TestMethod]
        public void TestIfFactoryIsUsingEmployeeCalculator()
        {
            Mock<IEmployeePayrollCalculator> empPayrollCalc = new Mock<IEmployeePayrollCalculator>();

            EmployeePayrollItem inputPayrollItem = new Svc.Payroll.EmployeePayrollItem();

            empPayrollCalc.Setup(x => x.CalculateEmployeePayroll(inputPayrollItem)).Returns(new CalculatedPayrollItem());

            var payrollFactory = new PayrollFactory(empPayrollCalc.Object);

            var calcValue = payrollFactory.CalculatePayroll(inputPayrollItem);

            Assert.IsNotNull(calcValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "")]
        public void TestIfThrowErrorIfCalculatorNotImplemented()
        {
            Mock<IInput> empInput = new Mock<IInput>();

            Mock<IEmployeePayrollCalculator> empPayrollCalc = new Mock<IEmployeePayrollCalculator>();

            EmployeePayrollItem inputPayrollItem = new Svc.Payroll.EmployeePayrollItem();

            empPayrollCalc.Setup(x => x.CalculateEmployeePayroll(inputPayrollItem)).Returns(new CalculatedPayrollItem());

            var payrollFactory = new PayrollFactory(empPayrollCalc.Object);

            var calcValue = payrollFactory.CalculatePayroll(empInput.Object);

        }
    }
}
