### Unit Test Cases for Payroll BLL

## EmployeePayrollCalculatorTest
- TestCorrectEmployeeIncomeTax  test if the income tax formula will return the correct and expected decimal value
- TestSalaryNotInRangeForIncomeTax   check if BLL returns an argument exception if requested salary for computation is not implemented or confugured or simply does not exist.
- TestIfNoTaxFormula  check if BLL returns an argument exception if no tax formula configured in an application that is using this BLL
- TestGrossIncome   Check grossincome calculation
- TestNetIncome  Check NetIncome calculation
- TestSuper  check super calculation


## IncomeTaxTest
* Too lazy to type. 
All these are just tests if implementations return expected value based on income levels & validation tests if the supplied annual income is below or max the set min/max configuration
