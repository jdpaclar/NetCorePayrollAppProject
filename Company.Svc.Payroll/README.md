### Business Entity Svc Models

models are segregated. Just a practice i learned from a dev contractor for code organization.
Service documents can be placed here. 
If an application requires an XML Schema Def, its easy to maintain it here and segregated from the rest of the application project.


## Custom Attribute
Are data annotation attributes to help validate properties

## Extensions
- ToEmployeePayrollItems - parse a csv line string into a EmployeePayrollItem object

## Interface
- IInput - interface created so that its easy to change Input model format, just implement the IInput Interface
- IResult - interface created so that its easy to change Result model format, just implement this.

## PostMan
- Sample postman .json file to test web api

