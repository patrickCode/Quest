angular.module("common", []);
angular.module("metadata", ["common"]);
angular.module("shell", ["common", "metadata"]);
angular.module("question", ["common"]);
angular.module("dashboard", ["common", "question"]);

angular.module("quest", ["ui.router", "ngMessages", "ui.bootstrap", "ui.toggle", "AdalAngular", "shell", "common", "metadata", "question", "dashboard"])
    .config([
        '$stateProvider', '$urlRouterProvider', 'adalAuthenticationServiceProvider', '$httpProvider', '$locationProvider', 'categoriesDataProvider', 'questionTypesDataProvider', 'answerTypesDataProvider', 'difficultyLevelsDataProvider',
        function ($stateProvider, $urlRouterProvider, adalAuthenticationService, $httpProvider, $locationProvider, categoriesData, questionTypesData, answerTypesData, difficultyLevelsData) {

            $locationProvider.hashPrefix('');

            $urlRouterProvider
                .when("/shell/questions", "/shell/questions/public")
                .otherwise("/shell/questions/public");

            $stateProvider
                .state("shell", {
                    url: "/shell",
                    templateUrl: "templates/shell/shell.html",
                    controller: "shellCtrl",
                    resolve: {
                        categories: function (categoriesData) {
                            return categoriesData.getAllCategories();
                        },
                        questionTypes: function (questionTypesData) {
                            return questionTypesData.getAllQuestionTypes();
                        },
                        answerTypes: function (answerTypesData) {
                            return answerTypesData.getAllAnswerTypes();
                        },
                        difficultyLevels: function (difficultyLevelsData) {
                            return difficultyLevelsData.getAllDifficultyLevels();
                        }
                    },
                    requireADLogin: true
                })
                    .state("dashboard", {
                        parent: "shell",
                        url: "/dashboard",
                        templateUrl: "templates/dashboard/dashboard.html",
                        controller: "dashboardCtrl",
                        requireADLogin: true
                    })
                    .state("questions", {
                        parent: "shell",
                        url: "/questions",
                        templateUrl: "templates/questions/questionsShell.html",
                        requireADLogin: true
                    })
                        .state("public", {
                            parent: "questions",
                            url: "/public",
                            templateUrl: "templates/questions/publicQuestions.html",
                            controller: "publicQuestionsCtrl",
                            requireADLogin: true
                        })
                        .state("view", {
                            parent: "questions",
                            url: "/{id}/view",
                            templateUrl: "templates/questions/viewQuestion.html",
                            controller: "viewQuestionCtrl",
                            requireADLogin: true
                        })
                        .state("add", {
                            parent: "questions",
                            url: "/add",
                            templateUrl: "templates/questions/addQuestion.html",
                            controller: "addQuestionCtrl",
                            requireADLogin: true
                        });

            var endpoints = {
                "api": "7f3b10af-0762-45dc-8b7e-c380ad852dc9"
            };

            adalAuthenticationService.init({
                instance: 'https://login.microsoftonline.com/',
                tenant: 'microsoft.onmicrosoft.com',
                clientId: '7f3b10af-0762-45dc-8b7e-c380ad852dc9',
                cacheLocation: 'localStorage',
                //endpoints: endpoints
            }, $httpProvider);
        }
    ]);