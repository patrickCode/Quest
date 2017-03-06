(function (module) {

    var questionsCategoryFilter = function() {
        return function (items, categoryCode) {
            if (items === undefined || items === null)
                return [];

            if (categoryCode === undefined || categoryCode === null) {
                return items;
            }

            if (categoryCode === "None")
                return items;

            return _.filter(items, function (item) {
                return _.some(item.categories, function (category) {
                    return category.code === categoryCode;
                })
            });

        }
    }

    module.filter("questionsCategoryFilter", questionsCategoryFilter);

}(angular.module("question")))