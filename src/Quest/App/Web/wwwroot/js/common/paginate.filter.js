(function (module) {

    var paginateFilter = function () {
        return function (items, pageNumber, pageSize) {
            if (items === undefined || items === null)
                return [];

            if (pageNumber === undefined || pageNumber === null || !angular.isNumber(pageNumber))
                return items;

            if (pageSize === undefined || pageSize === null || !angular.isNumber(pageSize))
                return items;

            var startIndex = (pageNumber - 1) * pageSize;
            var endIndex = pageNumber * pageSize;

            return items.slice(startIndex, endIndex);
        }
    }

    module.filter("paginate", paginateFilter);

}(angular.module("common")))