(function (module) {

    var dupQuestion = function ($q, questionsData) {

        var validateDuplicationQuestion = function (value) {
            var deferred = $q.defer();

            questionsData.getQuestionByValue(value)
                .then(function (data) {
                    if (data === undefined || data === null || data === "")
                        deferred.resolve(true);
                    else
                        deferred.reject(false);
                })
                .catch(function (error) {
                    deferred.reject(false);
                });

            return deferred.promise;
        }

        return {
            restrict: "A",
            require: "ngModel",
            link: function (scope, element, attr, ngModel) {
                ngModel.$asyncValidators.dupQuestion = validateDuplicationQuestion;
            }
        }
    }

    module.directive("dupQuestion", dupQuestion);

}(angular.module("question")))