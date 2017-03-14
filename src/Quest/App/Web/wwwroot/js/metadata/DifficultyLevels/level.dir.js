(function (module) {

    var levelDirective = function (difficultyLevelsData) {
        return {
            restrict: "A/E",
            templateUrl: "templates/metadata/level.dir.html",
            scope: {
                index: "=",
                onlyTitle: "@"
            },
            link: function (scope) {
                scope.showIndex = scope.onlyTitle === "false";
                scope.requiredLevel = difficultyLevelsData.getDifficultyLevelByIndex(scope.index);
            }
        }
    }

    module.directive("level", levelDirective);

}(angular.module("metadata")))