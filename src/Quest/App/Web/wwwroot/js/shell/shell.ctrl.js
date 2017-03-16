(function (module) {

    var shellCtrl = function ($scope, adalAuthenticationService) {
        $scope.userInfo = adalAuthenticationService.userInfo;
    }

    module.controller("shellCtrl", shellCtrl);

}(angular.module("shell")))