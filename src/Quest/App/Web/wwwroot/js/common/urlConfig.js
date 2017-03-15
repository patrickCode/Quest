(function (module) {

    var urlConfig = function () {
        var baseUrl = "api/";

        //Questions
        var questionsByUserIdUrl = "users/{userId}/questions";
        var questionsByUserId = function (userId) {
            return baseUrl + (questionsByUserIdUrl.replace("{userId}", userId));
        }
        var getQuestionByIdUrl = "questions/{id}";
        var getQuestionById = function (id) {
            return baseUrl + (getQuestionByIdUrl.replace("{id}", id));
        }
        var addNewQuestionUrl = baseUrl + "questions";
        var getQuestionByValueUrl = "questions/{value}?type=question"
        var getQuestionByValue = function (value) {
            return baseUrl + (getQuestionByValueUrl.replace("{value}", value));
        }
        var getPublicQuestionsUrl = baseUrl + "questions?publicVisibility=true";

        //Metadata
        var getAllCategoriesUrl = baseUrl + "categories";
        var getAllQuestionTypesUrl = baseUrl + "questionTypes";
        var getAllAnswerTypesUrl = baseUrl + "answerTypes";
        var getAllDifficultyLevelsUrl = baseUrl + "difficultyLevels";


        return {
            questionsByUserId: questionsByUserId,
            addNewQuestion: addNewQuestionUrl,
            getPublicQuestions: getPublicQuestionsUrl,
            getQuestionByValue: getQuestionByValue,
            getQuestionById: getQuestionById,
            editQuestion: addNewQuestionUrl,
            deleteQuestion: getQuestionById,

            getAllCategories: getAllCategoriesUrl,
            getAllQuestionTypes: getAllQuestionTypesUrl,
            getAllAnswerTypes: getAllAnswerTypesUrl,
            getAllDifficultyLevels: getAllDifficultyLevelsUrl
        }
    }

    module.factory("urlConfig", urlConfig);

}(angular.module("common")))