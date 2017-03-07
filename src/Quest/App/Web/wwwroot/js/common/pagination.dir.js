(function (module) {

    var pagination = function () {
        return {
            restrict: "A/E",
            scope: {
                currentPage: "=",
                pageSize: "@",
                totalRecords: "="
            },
            templateUrl: "templates/common/pagination.dir.html",
            link: function (scope) {
                scope.currentPageTemp = scope.currentPage;
                scope.$watch('totalRecords', function () {
                    scope.totalPages = parseInt(scope.totalRecords / scope.pageSize) +
                        (scope.totalRecords > scope.pageSize ? (scope.totalRecords % scope.pageSize !== 0 ? 1 : 0) : 0);
                    if (scope.currentPage > scope.totalPages) {
                        scope.currentPage = 1;
                        scope.currentPageTemp = scope.currentPage;
                    }
                    
                });
                scope.pageNumberChanged = function () {
                    if (scope.currentPageTemp !== undefined && scope.currentPageTemp !== null && scope.currentPageTemp !== "" && angular.isNumber(scope.currentPageTemp)) {
                        if (scope.currentPageTemp <= scope.totalPages) {
                            scope.currentPage = scope.currentPageTemp;
                        }
                        else {

                        }
                    }
                }

                scope.changePage = function (pageNumber) {
                    if (pageNumber !== undefined && pageNumber !== null && pageNumber !== "" && angular.isNumber(pageNumber)) {
                        if (pageNumber > 0 && pageNumber <= scope.totalPages) {
                            scope.currentPage = pageNumber;
                            scope.currentPageTemp = scope.currentPage;
                        }
                    }
                }
            }
        }
    }

    module.directive("pagination", pagination);

}(angular.module("common")))