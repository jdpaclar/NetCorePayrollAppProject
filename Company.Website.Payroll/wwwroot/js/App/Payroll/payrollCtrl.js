(function () {
    "use strict";

    angular
        .module("payrollApp")
        .controller("payrollCtrl", ["payrollResource", payrollCtrl]);


    function payrollCtrl(payrollResource) {
        var vm = this;

        vm.calculatePayroll = function () {
            vm.loader = true;

            
        }
    }
}());