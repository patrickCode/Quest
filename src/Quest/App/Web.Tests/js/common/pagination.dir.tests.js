/// <reference path="../../../web/wwwroot/lib/angular/angular.js" />    
/// <reference path="../../../web/wwwroot/lib/qunit/qunit/qunit.js" />

/// <reference path="../../../web/wwwroot/js/app.js" />
/// <reference path="../../../web/wwwroot/js/common/pagination.dir.js" />

var injector;
var scope;
var compileSvc;
var templateCacheSvc;
var directiveSyntax = "<pagination current-page='currentPage' page-size='{{pageSize}}' total-records='totalRecords'></pagination>";
var compiledTemplate = "{{currentPage}} - {{pageSize}} - {{totalRecords}}"

QUnit.module("Pagination Tests", {
    setup: function () {
        injector = angular.injector(['ng', 'common']);
        scope = injector.get("$rootScope").$new();
        compileSvc = injector.get("$compile");
        templateCacheSvc = injector.get("$templateCache");
        //templateCacheSvc.put("templates/common/pagination.dir.html", compiledTemplate);
    }, teardown: function () {
        injector = null;
        scope = null;
        compileSvc = null;
        templateCacheSvc = null;
    }
});

QUnit.test("Load Directive", function () {
    scope.currentPage = 1;
    scope.pageSize = 10;
    scope.totalRecords = 100;
    templateCacheSvc.put("templates/common/pagination.dir.html", compiledTemplate);
    var actualDOM = compileSvc(directiveSyntax)(scope);

    scope.$digest();

    var expectedDOM = "1 - 10 - 100";
    equal(expectedDOM, actualDOM.html());
});

QUnit.test("Update Total Pages", function () {
    scope.currentPage = 1;
    scope.pageSize = 10;
    scope.totalRecords = 100;
    templateCacheSvc.put("templates/common/pagination.dir.html", "{{totalPages}}");
    var actualDOM = compileSvc(directiveSyntax)(scope);

    scope.$digest();
    equal("10", actualDOM.html())

    scope.totalRecords = 120;
    scope.$digest();
    equal("12", actualDOM.html())
});