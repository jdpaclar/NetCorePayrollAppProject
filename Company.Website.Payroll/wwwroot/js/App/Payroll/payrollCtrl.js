(function () {
    "use strict";

    angular
        .module("payrollApp")
        .controller("payrollCtrl", ["$uibModal", "$http", payrollCtrl]);


    function payrollCtrl($uibModal, $http) {
        var vm = this;

        vm.loadPayroll = function () {
            vm.showError = false;
            
            var post = $http({
                method: "POST",
                url: "/Employee/CalculateEmployeePayroll",
                dataType: "json",
                headers: {
                    "Content-Type": "application/json"
                },
                data: JSON.stringify({
                    "FirstName": vm.FirstName,
                    "LastName": vm.LastName,
                    "AnnualSalary": vm.AnnualSalary,
                    "SuperRate": vm.SuperRate,
                    "DateInput": vm.DateInput
                }),
                headers: { "Content-Type": "application/json" }
            }).then(function successCallback(response) {
                
                if (response.data.isSuccess) {

                    var modalInstance = $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: '/js/App/Payroll/calculatedPayrollModal.html',
                        controller: 'calculatedPayrollModalCtrl',
                        controllerAs: 'vm',
                        resolve: {
                            calculatedItemResult: function () {
                                return response.data.calculatedPayroll;
                            }
                        }
                    });
                }
                else
                {
                    if (response.data.listMessage != null) 
                         vm.ErrorMessage = response.data.listMessage;
                    else
                        vm.ErrorMessage = response.data.message;

                    vm.showError = true;
                }

            }, function errorCallback(response) {
                
            });

        }
    }
}());
