(function (module) {

    var questionTypeData = function ($q, proxy, sharedContext, urlConfig) {

        var getAllQuestionTypes = function (rebuildCache) {
            if (rebuildCache === undefined || rebuildCache === null)
                rebuildCache = false;
            var deferred = $q.defer();

            if (rebuildCache === false) {
                var questionTypes = getAllCachedQuestionTypes();
                if (questionTypes !== undefined && questionTypes !== null) {
                    deferred.resolve(questionTypes);
                    return deferred.promise;
                }
            }
            var url = urlConfig.getAllQuestionTypes;
            proxy.get(url)
                .then(function (response) {
                    cacheQuestionTypes(response.data);
                    deferred.resolve(response.data);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        var cacheQuestionTypes = function (questionTypes) {
            sharedContext.setValue("key-questionTypes", questionTypes);
        }

        var getAllCachedQuestionTypes = function () {
            return sharedContext.getValue("key-questionTypes");
        }

        return {
            getAllQuestionTypes: getAllQuestionTypes,
            getAllCachedQuestionTypes: getAllCachedQuestionTypes
        }

    }

    module.factory("questionTypesData", questionTypeData);

}(angular.module("metadata")))