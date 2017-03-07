(function (module) {

    var categoriesData = function ($q, proxy, sharedContext, urlConfig) {

        var getAllCategories = function (rebuildCache) {
            if (rebuildCache === undefined || rebuildCache === null)
                rebuildCache = false;
            var deferred = $q.defer();

            if (rebuildCache === false) {
                var categories = getAllCachedCategories();
                if (categories !== undefined && categories !== null) {
                    deferred.resolve(categories);
                    return deferred.promise;
                }
            }
            var url = urlConfig.getAllCategories;
            proxy.get(url)
                .then(function (response) {
                    cacheCategories(response.data);
                    deferred.resolve(response.data);
                }, function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        var cacheCategories = function (categories) {
            sharedContext.setValue("key-categories", categories);
        }

        var getAllCachedCategories = function () {
            return sharedContext.getValue("key-categories");
        }

        return {
            getAllCategories: getAllCategories,
            getAllCachedCategories: getAllCachedCategories
        }

    }

    module.factory("categoriesData", categoriesData);

}(angular.module("metadata")))