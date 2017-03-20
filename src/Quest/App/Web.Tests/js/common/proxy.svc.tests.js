/// <reference path="../../../web/wwwroot/lib/angular/angular.js" />
/// <reference path="../../../web/wwwroot/lib/qunit/qunit/qunit.js" />
/// <reference path="../../../web/wwwroot/lib/angular-mocks/angular-mocks.js" />

/// <reference path="../../../web/wwwroot/js/app.js" />
/// <reference path="../../../web/wwwroot/js/common/proxy.svc.js" />

var injector;
var httpBackend;
var proxySvc;

QUnit.module("Proxy Tests", {
    setup: function () {
        injector = angular.injector(['ng', 'ngMockE2E', 'common']);
        httpBackend = injector.get("$httpBackend");
        proxySvc = injector.get("proxy");
    },
    teardown: function () {
        injector = null;
        httpBackend = null;
        proxySvc = null;
    }
});

QUnit.test("TestGetCall", function () {
    var url = "http://questhunt.azurewebsites.net/api/questions";
    var expectedResponse = {
        prop1: "some_value",
        prop2: "some_value"
    };
    httpBackend.whenGET(url)
        .respond(expectedResponse);

    expect(1);
    proxySvc.get(url)
        .then(function (actualResponse) {
            deepEqual(expectedResponse, actualResponse.data);
            start();
        });
    stop();
});

QUnit.test("TestFailingGetCall", function () {
    var url = "http://questhunt.azurewebsites.net/api/questions";
    var expectedResponse = {
        error: "Some dummy error"
    };
    httpBackend.whenGET(url)
        .respond(500, expectedResponse);

    expect(2);
    proxySvc.get(url)
        .then(null, function (actualResponse) {
            deepEqual(expectedResponse, actualResponse.data);
            equal(500, actualResponse.status);
            start();
        });
    stop();
});