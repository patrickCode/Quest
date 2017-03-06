(function (module) {

    var dashboardCtrl = function ($scope, questionsData) {

        $scope.userId = "pratikb@microsoft.com";
        $scope.questions = [];
        $scope.questionsLoading = false;
        $scope.errorOcurredWhileLoadingQuestions = false;
        $scope.errorMessage = "";

        var getUserQuestions = function () {
            $scope.questionsLoading = true;
            $scope.errorOcurredWhileLoadingQuestions = false;
            questionsData.getUserQuestions($scope.userId)
                .then(function (data) {
                    $scope.questions = data;
                    $scope.questionsLoading = false;
                })
                .catch(function (error) {
                    $scope.questionsLoading = false;
                    $scope.errorOcurredWhileLoadingQuestions = true;
                    $scope.errorMessage = error;
                });
        }

        var init = function () {
            $scope.getUserQuestions = getUserQuestions;
            $scope.getUserQuestions();
        }
        init();
    }

    module.controller("dashboardCtrl", dashboardCtrl);

}(angular.module("dashboard")))