(function (module) {

    var confirmQuestionDeletion = function ($uibModal) {
        return function (question) {
            var options = {
                templateUrl: "templates/questions/confirmQuestionDeletion.modal.html",
                controller: function () {
                    this.question = question;
                },
                controllerAs: "model"
            };
            return $uibModal.open(options).result;
        }
    };
    module.factory("confirmQuestionDeletion", confirmQuestionDeletion);


}(angular.module("question")))