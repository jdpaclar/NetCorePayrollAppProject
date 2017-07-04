(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("payrollResource", ["$resource", "appSettings", payrollResource]);

    function payrollResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/payrollcalculate");
    }

}());