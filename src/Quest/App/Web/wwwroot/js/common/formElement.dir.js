(function (module) {
    var setupDom = function (element) {
        var inputElement = element.querySelector("input, textarea, select, toggle");
        if (inputElement === null)
            return;
        var inputType = inputElement.getAttribute("type");
        if (inputType !== "checkbox" && inputType !== "radio" && inputType !== "toggle") {
            inputElement.classList.add("form-control");
        }

        var labelElement = element.querySelector("label");
        labelElement.classList.add("control-label");

        element.classList.add("form-group");
    }

    var getInputElement = function (element) {
        return element.querySelector("input, textarea, select, toggle");;
    }

    var getElementName = function (element) {
        var inputElement = element.querySelector("input, textarea, select, toggle");
        if (inputElement === null)
            return null;
        var elementName = inputElement.getAttribute("name");
        console.log(elementName);
        return elementName;
    }

    var watchForInvalidation = function (form, inputName) {
        return function () {
            if (form && form[inputName]) {
                return form[inputName].$invalid && form[inputName].$touched;
            }
        }
    }

    var updateInvalidateInput = function (element) {
        return function (isInvalid) {
            var inputElement = element[0].querySelector("input, textarea, select, toggle");
            if (isInvalid) {
                inputElement.classList.add("has-error");
            }
            else {
                inputElement.classList.remove("has-error");
            }
        }
    }

    var setupInvalidMessages = function (scope, element, $compile, form, inputElementName) {
        var message =
            "<div class = 'help-block' ng-messages='" + form.$name + "." + inputElementName + ".$error' ng-show=" + form.$name + "." + inputElementName + ".$touched>" +
                "<div ng-messages-include = 'templates/common/formInvalidMessages.html'></div>" +
            "</div>" + form.$name + "." + inputElementName + ".$touched" + "{{" + form.$name + "." + inputElementName + ".$touched}}";
        var compiledHtml = $compile(message)(scope);
        element.find("div").append(compiledHtml);
    }

    var link = function ($compile) {
        return function (scope, element, attr, formCtrl) {
            var htmlElement = element[0];
            setupDom(htmlElement);
            var elementName = getElementName(htmlElement);
            scope.$watch(watchForInvalidation(formCtrl, elementName), updateInvalidateInput(element));
            if (formCtrl[elementName].$errors !== {})
                setupInvalidMessages(scope, element, $compile, formCtrl, elementName);
        }
    }

    var formElement = function ($compile) {
        return {
            restrict: "A",
            require: "^form",
            link: link($compile)
        }
    }

    module.directive("formElement", formElement);

}(angular.module("common")))