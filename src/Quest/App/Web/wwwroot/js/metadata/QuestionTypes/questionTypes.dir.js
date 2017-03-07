(function (module) {

    var questionTypeDir = function () {
        return {
            restrict: "A/E",
            scope: {
                questionTypeCode: "@"
            },
            template: "<span><i class = 'glyphicon' ng-class='iconClass'></i></span>",
            link: function (scope) {
                var getQuestionTypeIconClass = function () {
                    switch (scope.questionTypeCode) {
                        case 'TXT': return "glyphicon-align-left";
                        case 'IMG': return "glyphicon-picture";
                        case 'AUD': return "glyphicon-volume-up";
                        case 'VID': return "glyphicon-film";
                        default: return "";
                    }
                }
                scope.iconClass = getQuestionTypeIconClass();
            }
        }
    }

    module.directive("questionType", questionTypeDir);

}(angular.module("metadata")))