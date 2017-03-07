(function (module) {

    var answerTypeDir = function () {
        return {
            restrict: "A/E",
            scope: {
                answerTypeCode: "@"
            },
            template: "<span><i class = 'glyphicon' ng-class='iconClass'></i></span>",
            link: function (scope) {
                var getAnswerTypeIconClass = function () {
                    switch (scope.answerTypeCode) {
                        case 'SUB': return "glyphicon-pencil";
                        case 'MCQ': return "glyphicon-list-alt";
                        default: return "";
                    }
                }
                scope.iconClass = getAnswerTypeIconClass();
            }
        }
    }

    module.directive("answerType", answerTypeDir);

}(angular.module("metadata")))