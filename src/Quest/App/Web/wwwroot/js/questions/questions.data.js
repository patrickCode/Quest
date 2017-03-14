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

        var getQuestionById = function (id) {
            var url = urlConfig.getQuestionById(id);
            var deferred = $q.defer();

            proxy.get(url)
                .then(function (response) {
                    deferred.resolve(response.data);
                }, function (error) {
                    deferred.reject(error)
                });
            return deferred.promise;
        }

        var getQuestionByValue = function (question) {
            var url = urlConfig.getQuestionByValue(encodeURIComponent(question));
            var deferred = $q.defer();

            proxy.get(url)
                .then(function (response) {
                    deferred.resolve(response.data);
                }, function (error) {
                    deferred.reject(error)
                });
            return deferred.promise;
        }

        var addQuestion = function (question) {
            var url = urlConfig.addNewQuestion;
            var deferred = $q.defer();

            //Audit
            question.createdBy = "pratikb@microsoft.com";
            question.lastModifiedBy = "pratikb@microsoft.com";
            var today = new Date();
            question.createdOn = today;
            question.lastModifedOn = today;

            var str = JSON.stringify(question);

            proxy.post(url, question)
                .then(function (response) {
                    deferred.resolve(response.data);
                }, function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        }

        var deleteQuestion = function (questionId) {
            var url = urlConfig.deleteQuestion(questionId);
            var deferred = $q.defer();

            proxy.del(url)
                .then(function (response) {
                    deferred.resolve(response.data);
                }, function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        }

        return {
            getUserQuestions: getUserQuestions,
            getQuestionByValue: getQuestionByValue,
            getQuestionById: getQuestionById,
            addQuestion: addQuestion,
            deleteQuestion: deleteQuestion
        }
    }

    module.factory("questionsData", questionData);

}(angular.module("question")))