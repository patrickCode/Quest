(function (module) {

    var editText = function ($uibModal) {
        return function (title, inputLabel, originalText) {
            var options = {
                templateUrl: "templates/common/editText.modal.html",
                controller: function () {
                    this.title = title;
                    this.inputLabel = inputLabel;
                    this.text = originalText;
                },
                controllerAs: "model"
            };
            return $uibModal.open(options).result;
        }
    }

    module.factory("editText", editText);

}(angular.module("common")))