(function (module) {

    var addQuestionController = function ($scope, $location, questionsData, appInsightsLogger, categories, questionTypes, answerTypes, difficultyLevels) {

        $scope.question = {
            "value": null,
            "difficultLevel": 100,
            "questionTypeCode": null,
            "answerTypeCode": null,
            "correctAnswer": null,
            "options": [],
            "categories": [],
            "tags": [],
            "createdBy": null,
            "createdOn": null,
            "lastModifiedBy": null,
            "lastModifedOn": null,
            "mediaUrl": null,
            "id": null
        };

        $scope.addingQuestion = false;
        $scope.errorOcurred = false;
        $scope.error = null;

        $scope.selectedCategories = [
            { category: null, subCategory: null },
            { category: null, subCategory: null },
            { category: null, subCategory: null }
        ];

        $scope.tags = null;

        var addQuestion = function () {
            $scope.addingQuestion = true;

            if ($scope.tags !== undefined && $scope.tags !== null)
                $scope.question.tags = $scope.tags.split(',');

            var acceptedCategoryCodes = _.reject($scope.selectedCategories, function (category) { return (category.category === null || category.category === "None") });

            $scope.question.categories = _.map(acceptedCategoryCodes, function (categoryCode) {
                var actualCategory = _.findWhere($scope.categories, { code: categoryCode.category });
                var actualSubCategory = _.findWhere(actualCategory.subCatgories, { code: categoryCode.subCategory });
                var addedCategory = {
                    value: actualCategory.name,
                    code: actualCategory.code,
                    subCategories: []
                };
                if (actualSubCategory !== undefined && actualSubCategory !== null) {
                    addedCategory.subCategories.push({
                        value: actualSubCategory.name,
                        code: actualSubCategory.code
                    });
                }

                return addedCategory;
            });

            questionsData.addQuestion($scope.question)
                .then(function (data) {
                    $scope.addingQuestion = false;
                    $location.path("shell/dashboard");
                }, function (error) {
                    $scope.addingQuestion = false;
                    $scope.errorOcurred = true;
                    $scope.error = error;
                    alert(error);
                });
        }

        var getSubcategories = function (categoryCode) {
            if (categoryCode === undefined || categoryCode === null)
                return [];
            var subCategories = (_.findWhere(categories, { code: categoryCode })).subCatgories;
            return subCategories;
        }

        var init = function () {
            appInsightsLogger.logView("Add Question", "questions/add");

            $scope.categories = categories;
            $scope.questionTypes = questionTypes;
            $scope.answerTypes = answerTypes;
            $scope.difficultyLevels = difficultyLevels;

            $scope.getSubcategories = getSubcategories;

            $scope.addQuestion = addQuestion;
        }

        init();

    }

    module.controller("addQuestionCtrl", addQuestionController);

}(angular.module("question")))