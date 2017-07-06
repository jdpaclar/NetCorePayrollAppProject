### Payroll Logic 

This section comprises the bulk of the calculation/business logic.

## Description

- PayrollFactory is where payroll types (Employee/Etc/Special) are being configured.
- EmployeePayrollCalculator is the concrete implementation of basic employee payroll rules/formula. Results per formula is not rounded and are raw. An extension method is called to round off the results.
- IncomeTaxFormulaClasses contains formula classes with a min/max salary levels. it implements the IIncomeTax interface so that developers can implement their own level of incometax.
- IncomeTaxFormulaConfig implements which incometax formula to be used in the application. Stored and configured in a list.

## Extensions

- DecimalRoundingExtension
  ToRoundedValue Rounds the decimal off into ceiling if greater than or equal to .50 cents and floor if less than .50 cents, segregated so that rounding rule change can easily be changed.
  
## Interfaces

- IEmployeePayrollCalculator
- IIncomeTax
- IIncomeTaxFormulaConfig
- IPayrollFactory
