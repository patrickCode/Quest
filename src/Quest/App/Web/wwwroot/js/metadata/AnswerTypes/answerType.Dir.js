(function (module) {

    var answerTypeDir = function (answerTypesData) {
        return {
            restrict: "A/E",
            scope: {
                answerTypeCode: "@",
                onlyIcon: "@"
            },
            templateUrl: "templates/metadata/answerType.dir.html",
            link: function (scope) {
                scope.showText = scope.onlyIcon === "false";
                scope.requiredAnswerType = answerTypesData.getAnswerTypeByCode(scope.answerTypeCode);
                scope.tooltip = scope.requiredAnswerType.name + ": " + scope.requiredAnswerType.description;
            }
        }
    }

    module.directive("answerType", answerTypeDir);

}(angular.module("metadata")))