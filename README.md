# Payroll Calculator Project
Simple project written on .NET Core.

## Getting Started


### Prerequisites
Project is written using vs2017 community edition.
Postman or any api testing tool (Fiddler.. etc) and example Postman .json located under Company.Svc.Payroll/PostMan

### Installing
- Install .net core as part of the installer package for vs2017


## Running Tests
Test can be run using the built in Test Explorer in vs2017

There are 2 test projects:
- Company.Test.PayrollBLL : Testing for Payroll calculator logic.
- Company.xUnitTest.Website.Payroll : Testing for AspNetCore controllers


## Deployment
Still working on an MSBuild proj file to publish .netCore csproj

Just do regular publish for webprojects on vs2017
Documentation for installing it on IIS: https://docs.microsoft.com/en-us/aspnet/core/publishing/iis

## Built With
BackEnd
- .Net Core 1.1

Front End
- AngularJs
- Bootstrap

Testing
- Moq
- xUnitTest

Nuget Packages
- Basically most of aspnetcore lib
- Automapper


## Authors

* **Jesse Dean Paclar**

## Road Map
- Add a Data Access Layer using Entity Framework code first approach
- Add a simple Mobile TACO app (Typescript/Angular/Cordova)
