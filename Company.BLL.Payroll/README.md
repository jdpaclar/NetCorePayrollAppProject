### Payroll Logic 

This section comprises the bulk of the calculation/business logic.

## Description

- PayrollFactory is where payroll types (Employee/Etc/Special) are being configured. If the company/dev wants to implement a new payroll calculator rules/formula, they can just configure it here.
- EmployeePayrollCalculator is the concrete implementation of basic employee payroll rules/formula. Results per formula is not rounded and are raw, reason being that if the dev wants to create a new rounding rule, the expected results/implementation from the formula wouldn't be affected. An extension method is called to round off the results.
- IncomeTaxFormulaClasses contains formula classes with a min/max salary levels. it implements the IIncomeTax interface so that developers can implement their own level of incometax.
- IncomeTaxFormulaConfig implements which incometax formula to be used in the application. Stored and configured in a list. (Would be cool if the configuration is being set at the application level and not on the BL, in that way an application can pick which tax levels it wants to use).

## Extensions

- DecimalRoundingExtension
  ToRoundedValue Rounds the decimal off into ceiling if greater than or equal to .50 cents and floor if less than .50 cents, segregated so that rounding rule change can easily be changed.
  
## Interfaces

- IEmployeePayrollCalculator
- IIncomeTax
- IIncomeTaxFormulaConfig
- IPayrollFactory
