(function () {
    "use strict";

    angular
        .module("payrollApp")
        .controller("payrollCtrl", ["payrollResource", "$uibModal", payrollCtrl]);


    function payrollCtrl(payrollResource, $uibModal) {
        var vm = this;

        vm.calculatePayroll = function () {
            vm.loader = true;
            
            var calculatedPayroll = null;
            
            payrollResource.query({
                
            }, function (data) {
                calculatedPayroll = data;
                vm.loader = false;
            });
            
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/js/App/Payroll/calculatedPayrollModal.html',
                controller: 'calculatedPayrollModalCtrl',
                controllerAs: 'vm',
                resolve: {
                    detailItems: function() {
                        return calculatedPayroll;
                    }
                }
            });
            
        }
    }
}());
