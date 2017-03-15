(function (module) {

    var publicQuestionsCtrl = function ($scope, $location, questionsData, appInsightsLogger) {

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
            pageSize: 20
        };

        $scope.totalCount = 0;
        $scope.paneltitle = "";

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

        var deleteQuestion = function ($event, question) {
            $event.stopPropagation();
            confirmQuestionDeletion(question)
                .then(function () {
                    question.deletingQuestion = true;
                    questionsData.deleteQuestion(question.id)
                        .then(function () {
                            question.deletingQuestion = false;
                            getUserQuestions();
                        })
                        .catch(function (exception) {
                            alert(exception);
                        });
                });

        }

        var showDetails = function (question) {
            var id = question.id;
            var detailsPath = "shell/questions/" + id + "/view";
            $location.path(detailsPath);
        }

        var init = function () {
            appInsightsLogger.logView("Public Questions", "/questions/public");

            $scope.getUserQuestions = getUserQuestions;
            $scope.changeOrder = changeOrder;
            $scope.getUserQuestions();
            $scope.addNewQuestion = addNewQuestion;
            $scope.showDetails = showDetails;
            $scope.deleteQuestion = deleteQuestion;
        }
        init();

    }

    module.controller("publicQuestionsCtrl", publicQuestionsCtrl);

}(angular.module("question")))