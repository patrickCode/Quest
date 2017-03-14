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
                    var levels = response.data;
                    if (levels !== undefined && levels !== null) {
                        setLevelClass(levels);
                        cacheDifficultyLevels(levels);
                        deferred.resolve(levels);
                    } else {
                        deferred.reject("Not Found");
                    }
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        var getDifficultyLevelByIndex = function (index) {
            var levels = getAllCachedDifficultyLevels();
            if (levels !== undefined && levels !== null) {
                return _.findWhere(levels, { levelIndex: index.toString() });
            }
        }

        var cacheDifficultyLevels = function (difficultyLevels) {
            sharedContext.setValue("key-difficultyLevels", difficultyLevels);
        }

        var getAllCachedDifficultyLevels = function () {
            return sharedContext.getValue("key-difficultyLevels");
        }

        var setLevelClass = function (levels) {
            _.forEach(levels, function (level) {
                var levelClass = getLevelClass(level.levelIndex);
                level.levelClass = levelClass;
            });
        }

        var getLevelClass = function (index) {
            switch (index) {
                case "100":
                case 100: return "btn-info";
                case "200":
                case 200: return "btn-warning";
                case "300":
                case 300: return "btn-primary";
                case "400":
                case 400: return "btn-danger";
                default: return undefined;
            }
        }

        return {
            getAllDifficultyLevels: getAllDifficultyLevels,
            getAllCachedDifficultyLevels: getAllCachedDifficultyLevels,
            getDifficultyLevelByIndex: getDifficultyLevelByIndex
        }
    }

    module.factory("difficultyLevelsData", questionTypeData);

}(angular.module("metadata")))