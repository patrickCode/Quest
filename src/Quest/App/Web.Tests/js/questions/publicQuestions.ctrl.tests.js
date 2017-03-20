/// <reference path="../../../web/wwwroot/lib/angular/angular.js" />
/// <reference path="../../../web/wwwroot/lib/qunit/qunit/qunit.js" />

/// <reference path="../../../web/wwwroot/js/app.js" />
/// <reference path="../../../web/wwwroot/js/questions/publicquestions.ctrl.js" />

var injector;
var qService;
var scope;
var locationService;
var defer;
var controllerService;
var mockQuestionsData;
var mockAiLogger;
var pathLogger;
var aiLoggedData;

QUnit.module("Public Questions Tests", {
    setup: function () {
        angular.module("common")
            .factory("$location", function () {
                return {
                    path: function (loc) {
                        pathLogger = loc;
                    }
                }
            });

        injector = angular.injector(['ng', 'common', 'question']);
        scope = injector.get('$rootScope').$new();
        locationService = injector.get("$location");
        controllerService = injector.get("$controller");
        qService = injector.get("$q");

    }, teardown: function () {
        injector = null;
        scope = null;
        controllerService = null;
        qService = null;
    }
});

QUnit.test("Load Controller", function () {
    var mockQuestion = [{ value: "Q1" }];
    mockQuestionsData = getMockQuestionsData(mockQuestion);
    mockAiLogger = getMockAiLogger();
    createController();
    scope.$apply();
    deepEqual(scope.questions, mockQuestion);
});

QUnit.test("Go to Questions Details", function () {
    var mockQuestion = [{ id: 1, value: "Q1" }];
    mockQuestionsData = getMockQuestionsData(mockQuestion);
    mockAiLogger = getMockAiLogger();
    createController();
    scope.$apply();

    scope.showDetails(mockQuestion[0]);
    scope.$apply();
    equal("shell/questions/1/view", pathLogger);
});

var getMockAiLogger = function () {
    return {
        logView: function (viewName, viewUrl) {
            aiLoggedData = "Logged Page View in AI -> View: " + viewName + " ViewURL: " + viewUrl;
        }
    }
}

var getMockQuestionsData = function (mockQuestions) {
    var defer = qService.defer();
    return {
        getPublicQuestions: function () {
            defer.resolve(mockQuestions);
            return defer.promise;
        }
    }
}

var createController = function () {
    controllerService("publicQuestionsCtrl", {
        $scope: scope,
        $location: locationService,
        questionsData: mockQuestionsData,
        appInsightsLogger: mockAiLogger
    })
}