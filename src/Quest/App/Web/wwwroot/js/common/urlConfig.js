(function (module) {

    var urlConfig = function () {
        var baseUrl = "api/";

        //Questions
        var questionsByUserIdUrl = "users/{userId}/questions";
        var questionsByUserId = function (userId) {
            return baseUrl + (questionsByUserIdUrl.replace("{userId}", userId));
        }

        var addNewQuestionUrl = baseUrl + "questions";

        //Metadata
        var getAllCategoriesUrl = baseUrl + "categories";
        var getAllQuestionTypesUrl = baseUrl + "questionTypes";
        var getAllAnswerTypesUrl = baseUrl + "answerTypes";
        var getAllDifficultyLevelsUrl = baseUrl + "difficultyLevels";


        return {
            questionsByUserId: questionsByUserId,
            addNewQuestion: addNewQuestionUrl,

            getAllCategories: getAllCategoriesUrl,
            getAllQuestionTypes: getAllQuestionTypesUrl,
            getAllAnswerTypes: getAllAnswerTypesUrl,
            getAllDifficultyLevels: getAllDifficultyLevelsUrl
        }
    }

    module.factory("urlConfig", urlConfig);

}(angular.module("common")))