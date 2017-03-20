/// <reference path="../../../web/wwwroot/lib/angular/angular.min.js" />
/// <reference path="../../../web/wwwroot/lib/qunit/qunit/qunit.js" />

/// <reference path="../../../web/wwwroot/js/app.js" />
/// <reference path="../../../web/wwwroot/js/questions/questions.data.js" />

QUnit.module("questionsDataTests", {
    setup: {},
    teardown: function(){
        injector = null;
        qService = null;
        defer = null;
        questionsData = null;
    }
});

var defer;
var injector;
var questionsData;

var setupWithProcessor = function (proxyProcessor, adalProcessor) {
    angular.module("common")
        .factory("proxy", proxyProcessor)
        .factory("urlConfig", function () {
            return {
                getPublicQuestions: "some_url",
                getQuestionById: function (id) {
                    return "some_url";
                }
            }
        });
    angular.module("AdalAngular", [])
        .factory("adalAuthenticationService", adalProcessor);

    injector = angular.injector(['ng', 'common', 'AdalAngular', 'question']);
    qService = injector.get("$q");
    defer = qService.defer();
    questionsData = injector.get("questionsData");
}

var setup = function () {
    angular.module("common").factory("proxy", function () {
        return {
            get: function (url) {
                defer.resolve({ data: ["abc"] });
                return defer.promise;
            }
        }
    })
    .factory("urlConfig", function () {
        return {
            getPublicQuestions: "some_url",
            getQuestionById: function (id) {
                return "some_url";
            }
        }
    });

    angular.module("AdalAngular", []).factory("adalAuthenticationService", function () {
        return {};
    });

    injector = angular.injector(['ng', 'common', 'AdalAngular', 'question']);
    qService = injector.get("$q");
    defer = qService.defer();
    questionsData = injector.get("questionsData");
}

QUnit.test("GetPublicQuestions", function (assert) {
    setup();
    expect(1);
    questionsData.getPublicQuestions()
        .then(function (data) {
            deepEqual(data, ["abc"]);
            start();
        });
    stop();
});

QUnit.test("GetQuestionById", function () {
    var dummyQuestion = {
        data: {
            value: "test question"
        }
    };
    var proxyProcessor = createDefaultProxyProcessor(dummyQuestion);
    var adalProcessor = createDefaultAdalProcessor();
    setupWithProcessor(proxyProcessor, adalProcessor);
    expect(1);
    questionsData.getQuestionById(1)
        .then(function (data) {
            deepEqual(data, dummyQuestion.data);
            start();
        });
    stop();
});


var createDefaultProxyProcessor = function (getData) {
    return function () {
        return {
            get: function (url) {
                defer.resolve(getData);
                return defer.promise;
            }
        }
    }
}

var createDefaultAdalProcessor = function () {
    return function () {
        return {};
    }
}