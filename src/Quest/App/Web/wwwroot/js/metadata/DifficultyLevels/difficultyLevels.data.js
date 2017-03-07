(function (module) {

    var questionTypeData = function ($q, proxy, sharedContext, urlConfig) {

        var getAllDifficultyLevels = function (rebuildCache) {
            if (rebuildCache === undefined || rebuildCache === null)
                rebuildCache = false;
            var deferred = $q.defer();

            if (rebuildCache === false) {
                var difficultyLevels = getAllCachedDifficultyLevels();
                if (difficultyLevels !== undefined && difficultyLevels !== null) {
                    deferred.resolve(difficultyLevels);
                    return deferred.promise;
                }
            }
            var url = urlConfig.getAllDifficultyLevels;
            proxy.get(url)
                .then(function (response) {
                    cacheDifficultyLevels(response.data);
                    deferred.resolve(response.data);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        var cacheDifficultyLevels = function (difficultyLevels) {
            sharedContext.setValue("key-difficultyLevels", difficultyLevels);
        }

        var getAllCachedDifficultyLevels = function () {
            return sharedContext.getValue("key-difficultyLevels");
        }

        return {
            getAllDifficultyLevels: getAllDifficultyLevels,
            getAllCachedDifficultyLevels: getAllCachedDifficultyLevels
        }
    }

    module.factory("difficultyLevelsData", questionTypeData);

}(angular.module("metadata")))