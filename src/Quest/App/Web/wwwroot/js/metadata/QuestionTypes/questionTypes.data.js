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
                    var questionTypes = response.data;
                    if (questionTypes !== undefined || questionTypes !== null) {
                        setQuestionTypeIcons(questionTypes);
                        cacheQuestionTypes(questionTypes);
                        deferred.resolve(questionTypes);
                    } else {
                        deferred.reject("Not Found");
                    }
                    
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        var getQuestionTypeByCode = function (code) {
            var questionTypes = getAllCachedQuestionTypes();
            if (questionTypes !== undefined || questionTypes !== null) {
                return _.findWhere(questionTypes, { code: code });
            }
        }

        var cacheQuestionTypes = function (questionTypes) {
            sharedContext.setValue("key-questionTypes", questionTypes);
        }

        var getAllCachedQuestionTypes = function () {
            return sharedContext.getValue("key-questionTypes");
        }

        var setQuestionTypeIcons = function (questionTypes) {
            _.forEach(questionTypes, function (questionType) {
                var iconClass = getQuestionTypeIconClass(questionType.code);
                questionType.iconClass = iconClass;
            });
        }

        var getQuestionTypeIconClass = function (code) {
            switch (code) {
                case 'TXT': return "glyphicon-align-left";
                case 'IMG': return "glyphicon-picture";
                case 'AUD': return "glyphicon-volume-up";
                case 'VID': return "glyphicon-film";
                default: return "";
            }
        }

        return {
            getAllQuestionTypes: getAllQuestionTypes,
            getAllCachedQuestionTypes: getAllCachedQuestionTypes,
            getQuestionTypeByCode: getQuestionTypeByCode
        }

    }

    module.factory("questionTypesData", questionTypeData);

}(angular.module("metadata")))