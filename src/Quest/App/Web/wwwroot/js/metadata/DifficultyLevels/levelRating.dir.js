(function (module) {

    var levelRating = function () {
        return {
            restrict: "A/E",
            scope: {
                currentRating: "@",
                changed: "&"
            },
            templateUrl: "templates/metadata/levelRating.dir.html",
            link: function (scope) {
                scope.level = "Basic";
                scope.currentRate = scope.currentRating;
                scope.ratingChanged = function (currentRate) {
                    scope.changed({ newRating: currentRate });
                }
            }
        }
    }

    module.directive("levelRating", levelRating);

}(angular.module("metadata")))