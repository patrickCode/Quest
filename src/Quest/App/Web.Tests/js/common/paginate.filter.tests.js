/// <reference path="../../../web/wwwroot/lib/angular/angular.js" />    
/// <reference path="../../../web/wwwroot/lib/qunit/qunit/qunit.js" />

/// <reference path="../../../web/wwwroot/js/app.js" />
/// <reference path="../../../web/wwwroot/js/common/paginate.filter.js" />

var injector;
var filterSvc;
var paginationFilter;

QUnit.module("Paginate Filter Tests", {
    setup: function () {
        injector = angular.injector(['ng', 'common']);
        filterSvc = injector.get("$filter");
        paginationFilter = filterSvc("paginate");
    }, teardown: function () {
        injector = null;
        filterSvc = null;
        paginationFilter = null;
    }
});

QUnit.test("Get 2nd Page", function () {
    var items = ["I1", "I2", "I3", "I4", "I5", "I6"];
    var fitleredItems = paginationFilter(items, 2, 2);

    var expectedFilteredItems = ["I3", "I4"];
    equal(2, fitleredItems.length);
    deepEqual(expectedFilteredItems, fitleredItems);
});