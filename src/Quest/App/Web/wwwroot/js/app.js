angular.module("common", []);
angular.module("metadata", ["common"]);
angular.module("question", ["common"]);
angular.module("dashboard", ["common", "question"]);

angular.module("quest", ["ui.router", "common", "metadata", "question", "dashboard"])
    .config([
        '$stateProvider', '$urlRouterProvider', '$httpProvider', '$locationProvider', 'categoriesDataProvider',
        function ($stateProvider, $urlRouterProvider, $httpProvider, $locationProvider, categoriesData) {

            $urlRouterProvider
                .otherwise("/shell/dashboard");

            $stateProvider
                .state("shell", {
                    url: "/shell",
                    templateUrl: "templates/shell/shell.html",
                    resolve: {
                        categories: function (categoriesData) {
                            return categoriesData.getAllCategories();
                        }
                    }
                })
                    .state("dashboard", {
                        parent: "shell",
                        url: "/dashboard",
                        templateUrl: "templates/dashboard/dashboard.html",
                        controller: "dashboardCtrl"
                    })
                    .state("questions", {
                        parent: "shell",
                        url: "/questions",
                        templateUrl: "templates/questions/questionsShell.html"
                    })
                        .state("add", {
                            parent: "questions",
                            url: "/add",
                            templateUrl: "templates/questions/addQuestion.html",
                            controller: "addQuestionCtrl"
                        });
        }
    ]);