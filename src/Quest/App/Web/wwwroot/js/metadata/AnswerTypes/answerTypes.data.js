(function (module) {

    var answerTypeData = function ($q, proxy, sharedContext, urlConfig) {

        var getAllAnswerTypes = function (rebuildCache) {
            if (rebuildCache === undefined || rebuildCache === null)
                rebuildCache = false;
            var deferred = $q.defer();

            if (rebuildCache === false) {
                var answerTypes = getAllCachedAnswerTypes();
                if (answerTypes !== undefined && answerTypes !== null) {
                    deferred.resolve(answerTypes);
                    return deferred.promise;
                }
            }
            var url = urlConfig.getAllAnswerTypes;
            proxy.get(url)
                .then(function (response) {
                    cacheAnswerTypes(response.data);
                    deferred.resolve(response.data);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        var cacheAnswerTypes = function (answerTypes) {
            sharedContext.setValue("key-answerTypes", answerTypes);
        }

        var getAllCachedAnswerTypes = function () {
            return sharedContext.getValue("key-answerTypes");
        }

        return {
            getAllAnswerTypes: getAllAnswerTypes,
            getAllCachedAnswerTypes: getAllCachedAnswerTypes
        }

    }

    module.factory("answerTypesData", answerTypeData);

}(angular.module("metadata")))