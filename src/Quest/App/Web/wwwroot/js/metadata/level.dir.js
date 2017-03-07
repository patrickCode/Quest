(function (module) {

    var levelDirective = function () {
        return {
            restrict: "A/E",
            templateUrl: "templates/metadata/level.dir.html",
            scope: {
                index: "=",
            },
            link: function (scope) {

                var getLevelClass = function () {
                    switch (scope.index) {
                        case 100: return "btn-info";
                        case 200: return "btn-warning";
                        case 300: return "btn-primary";
                        case 400: return "btn-danger";
                        default: return undefined;
                    }
                }

                var levelClass = getLevelClass();
                if (levelClass === undefined)
                    index = "Undefined Level";
                else
                    scope.levelClass = levelClass;
            }
        }
    }

    module.directive("level", levelDirective);

}(angular.module("metadata")))