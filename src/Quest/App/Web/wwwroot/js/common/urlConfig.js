(function (module) {

    var urlConfig = function () {
        var baseUrl = "http://questhunt.azurewebsites.net/api/";

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