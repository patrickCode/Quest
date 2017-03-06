(function (module) {

    var questionData = function ($q, urlConfig, proxy) {
        var getUserQuestions = function (userId) {
            var url = urlConfig.questionsByUserId(userId);
            var deferred = $q.defer();

            proxy.get(url)
                .then(function (response) {
                    deferred.resolve(response.data);
                }, function (error) {
                    deferred.reject(error)
                });
            return deferred.promise;
        }

        return {
            getUserQuestions: getUserQuestions
        }
    }

    module.factory("questionsData", questionData);

}(angular.module("question")))