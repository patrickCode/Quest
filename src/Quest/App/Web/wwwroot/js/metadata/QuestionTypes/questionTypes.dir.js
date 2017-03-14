(function (module) {

    var questionTypeDir = function (questionTypesData) {
        return {
            restrict: "A/E",
            scope: {
                questionTypeCode: "@",
                onlyIcon: "@"
            },
            templateUrl: "templates/metadata/questionTypes.dir.html",
            link: function (scope) {
                scope.showText = scope.onlyIcon === "false";
                scope.requiredQuestionType = questionTypesData.getQuestionTypeByCode(scope.questionTypeCode);
                scope.tooltip = scope.requiredQuestionType.name + ": " + scope.requiredQuestionType.description;
            }
        }
    }

    module.directive("questionType", questionTypeDir);

}(angular.module("metadata")))