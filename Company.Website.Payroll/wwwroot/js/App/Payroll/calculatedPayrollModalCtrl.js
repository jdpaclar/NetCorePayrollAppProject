(function () {

    "use strict";

    angular.module("payrollApp")
        .controller("calculatedPayrollModalCtrl", calculatedPayrollModalCtrl);

    function calculatedPayrollModalCtrl($uibModalInstance, calculatedItemResult) {
        var vm = this;

        vm.close = function () {
            $uibModalInstance.dismiss('cancel');
        }
    }

}());