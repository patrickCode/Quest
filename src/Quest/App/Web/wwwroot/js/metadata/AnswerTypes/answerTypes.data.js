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
                    var answerTypes = response.data;
                    if (answerTypes !== undefined && answerTypes !== null) {
                        setAnswerTypeIcons(answerTypes);
                        cacheAnswerTypes(answerTypes);
                        deferred.resolve(answerTypes);
                    } else {
                        deferred.reject("Not Found");
                    }
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        var getAnswerTypeByCode = function (code) {
            var answerTypes = getAllCachedAnswerTypes();
            if (answerTypes !== undefined || answerTypes !== null) {
                return _.findWhere(answerTypes, { code: code });
            }
        }

        var cacheAnswerTypes = function (answerTypes) {
            sharedContext.setValue("key-answerTypes", answerTypes);
        }

        var getAllCachedAnswerTypes = function () {
            return sharedContext.getValue("key-answerTypes");
        }

        var setAnswerTypeIcons = function (answerTypes) {
            _.forEach(answerTypes, function (answerType) {
                var iconClass = getAnswerTypeIconClass(answerType.code);
                answerType.iconClass = iconClass;
            });
        }

        var getAnswerTypeIconClass = function (code) {
            switch (code) {
                case 'SUB': return "glyphicon-pencil";
                case 'MCQ': return "glyphicon-list-alt";
                default: return "";
            }
        }

        return {
            getAllAnswerTypes: getAllAnswerTypes,
            getAllCachedAnswerTypes: getAllCachedAnswerTypes,
            getAnswerTypeByCode: getAnswerTypeByCode
        }

    }

    module.factory("answerTypesData", answerTypeData);

}(angular.module("metadata")))