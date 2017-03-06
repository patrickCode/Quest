(function (module) {

    var urlConfig = function () {
        var baseUrl = "http://localhost:7578/api/";

        var questionsByUserIdUrl = "users/{userId}/questions";
        var questionsByUserId = function (userId) {
            return baseUrl + (questionsByUserIdUrl.replace("{userId}", userId));
        }

        return {
            questionsByUserId: questionsByUserId
        }
    }

    module.factory("urlConfig", urlConfig);

}(angular.module("common")))