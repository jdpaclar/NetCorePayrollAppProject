(function () {

    "use strict";

    angular.module("payrollApp")
        .controller("calculatedPayrollModalCtrl", calculatedPayrollModalCtrl);

    function calculatedPayrollModalCtrl($uibModalInstance, calculatedItemResult) {
        var vm = this;
        
        vm.FullName = calculatedItemResult.fullName;
        vm.PayPeriod = calculatedItemResult.payPeriod;
        vm.GrossIncome = calculatedItemResult.grossIncome;
        vm.IncomeTax = calculatedItemResult.incomeTax;
        vm.NetIncome = calculatedItemResult.netIncome;
        vm.Super = calculatedItemResult.super;
        
        vm.close = function () {
            $uibModalInstance.dismiss('cancel');
        }
    }

}());
