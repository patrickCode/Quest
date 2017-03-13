(function (module) {

    var dashboardCtrl = function ($scope, $location, questionsData, appInsightsLogger, categories) {

        $scope.userId = "pratikb@microsoft.com";
        $scope.questions = [];
        $scope.questionsLoading = false;
        $scope.errorOcurredWhileLoadingQuestions = false;
        $scope.errorMessage = "";
        $scope.availableCategories = [];
        $scope.selectedCategory = "None";

        $scope.orderOn = "difficultLevel"
        $scope.orderByDesc = false;

        $scope.pagination = {
            currentPage: 1,
            pageSize: 3
        };

        $scope.totalCount = 0;

        var changeOrder = function (orderOn) {
            if ($scope.orderOn === orderOn) {
                $scope.orderByDesc = !$scope.orderByDesc;
            }
            else {
                $scope.orderOn = orderOn;
                $scope.orderByDesc = false;
            }
        }

        var getUserQuestions = function () {
            $scope.questionsLoading = true;
            $scope.errorOcurredWhileLoadingQuestions = false;
            questionsData.getUserQuestions($scope.userId)
                .then(function (data) {
                    $scope.totalCount = data.length;
                    $scope.questions = data;
                    $scope.availableCategories = _.uniq(_.flatten(
                        _.map($scope.questions, function (question) {
                            return question.categories;
                        })),
                        _.property('code'));
                    $scope.questionsLoading = false;
                })
                .catch(function (error) {
                    $scope.questionsLoading = false;
                    $scope.errorOcurredWhileLoadingQuestions = true;
                    $scope.errorMessage = error;
                });
        }

        var addNewQuestion = function () {
            $location.path("shell/questions/add");
        }

        var init = function () {
            appInsightsLogger.logView("Dashboard", "/dashboard");

            $scope.getUserQuestions = getUserQuestions;
            $scope.changeOrder = changeOrder;
            $scope.getUserQuestions();
            $scope.addNewQuestion = addNewQuestion;
        }
        init();
    }

    module.controller("dashboardCtrl", dashboardCtrl);

}(angular.module("dashboard")))