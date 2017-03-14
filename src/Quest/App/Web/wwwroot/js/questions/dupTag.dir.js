(function (module) {

    var dupTag = function () {
        return {
            restrict: "A",
            require: "ngModel",
            link: function (scope, element, attr, ngModel) {
                ngModel.$validators.dupTag = function (value) {
                    if (value === undefined || value === null || value === "")
                        return true;
                    var tags = value.split(',');
                    if (tags.length === 1)
                        return true;
                    var uniqueTags = _.uniq(tags);
                    return uniqueTags.length === tags.length;
                }
            }
        }
    }

    module.directive("dupTag", dupTag)

}(angular.module("question")))