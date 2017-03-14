(function (module) {

    var viewQuestionCtrl = function ($scope, $location, $stateParams, questionsData, confirmQuestionDeletion, editText, appInsightsLogger, categories, questionTypes, answerTypes, difficultyLevels) {
        $scope.questionId = null;
        $scope.question = null;
        $scope.questionLoading = false;
        $scope.errorOcurred = false;
        $scope.errorMessage = null;

        $scope.deletingQuestion = false;
        $scope.editingQuestion = false;

        var deleteQuestion = function () {
            confirmQuestionDeletion($scope.question)
                .then(function () {
                    $scope.deletingQuestion = true;
                    questionsData.deleteQuestion($scope.question.id)
                        .then(function () {
                            $scope.deletingQuestion = false;
                            $location.path("shell/dashboard");
                        })
                        .catch(function (exception) {
                            alert(exception);
                        });
                });

        }

        var editQuestion = function () {

        }

        var editQuestionText = function () {
            editText("Edit Question", "Question", $scope.question.value)
                .then(function (updatedQuestion) {
                    $scope.editingQuestion = true;
                    $scope.question.value = updatedQuestion;
                    questionsData.editQuestion($scope.question)
                        .then(function (data) {
                            $scope.editingQuestion = false;
                        })
                        .catch(function (exception) {
                            $scope.editingQuestion = false;
                            $scope.errorOcurred = true;
                            alert(exception);
                        });
                })
        }

        var editAnswerText = function () {
            editText("Edit Answer", "Answer", $scope.question.correctAnswer)
                .then(function (updatedAnswer) {
                    $scope.editingQuestion = true;
                    $scope.question.correctAnswer = updatedAnswer;
                    questionsData.editQuestion($scope.question)
                        .then(function (data) {
                            $scope.editingQuestion = false;
                        })
                        .catch(function (exception) {
                            $scope.editingQuestion = false;
                            $scope.errorOcurred = true;
                            alert(exception);
                        });
                })
        }

        var getSubCategories = function (categoryCode) {
            if (categoryCode === undefined || categoryCode === null)
                return [];
            var subCategories = (_.findWhere($scope.question.categories, { code: categoryCode })).subCategories;
            return subCategories;
        }

        var getQuestion = function () {
            $scope.questionLoading = true;
            questionsData.getQuestionById($scope.questionId)
                .then(function (data) {
                    $scope.questionLoading = false;
                    if (data === undefined || data === null || data === "") {
                        $scope.errorOcurred = true;
                        $scope.errorMessage = "No question exists with this ID. If the question was created recently then please try again after some time";
                        return;
                    }
                    $scope.question = data;
                })
                .catch(function (error) {
                    $scope.questionLoading = false;
                    $scope.errorOcurred = true;
                    alert(error);
                });
        }

        var init = function () {
            appInsightsLogger.logView("View Question", "/questions/" + $stateParams.id + "view");
            $scope.deleteQuestion = deleteQuestion;
            $scope.editQuestion = editQuestion;
            $scope.getQuestion = getQuestion;
            $scope.editQuestionText = editQuestionText;
            $scope.editAnswerText = editAnswerText;
            $scope.getSubCategories = getSubCategories;

            $scope.questionId = $stateParams.id;
            getQuestion();
        }

        init();
    }

    module.controller("viewQuestionCtrl", viewQuestionCtrl);

}(angular.module("question")))