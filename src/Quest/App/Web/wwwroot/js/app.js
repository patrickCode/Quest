angular.module("common", []);
angular.module("metadata", ["common"]);
angular.module("question", ["common"]);
angular.module("dashboard", ["common", "question"]);

angular.module("quest", ["ui.router", "ngMessages", "ui.bootstrap", "ui.toggle", "common", "metadata", "question", "dashboard"])
    .config([
        '$stateProvider', '$urlRouterProvider', '$httpProvider', '$locationProvider', 'categoriesDataProvider', 'questionTypesDataProvider', 'answerTypesDataProvider', 'difficultyLevelsDataProvider',
        function ($stateProvider, $urlRouterProvider, $httpProvider, $locationProvider, categoriesData, questionTypesData, answerTypesData, difficultyLevelsData) {

            $urlRouterProvider
                .when("/shell/questions", "/shell/questions/public")
                .otherwise("/shell/questions/public");

            $stateProvider
                .state("shell", {
                    url: "/shell",
                    templateUrl: "templates/shell/shell.html",
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
                        .state("public", {
                            parent: "questions",
                            url: "/public",
                            templateUrl: "templates/questions/publicQuestions.html",
                            controller: "publicQuestionsCtrl"
                        })
                        .state("view", {
                            parent: "questions",
                            url: "/{id}/view",
                            templateUrl: "templates/questions/viewQuestion.html",
                            controller: "viewQuestionCtrl"
                        })
                        .state("add", {
                            parent: "questions",
                            url: "/add",
                            templateUrl: "templates/questions/addQuestion.html",
                            controller: "addQuestionCtrl"
                        });
        }
    ]);