(function (module) {

    var sharedContext = function () {
        var sharedKvPair = {};

        var set = function (key, value) {
            sharedKvPair[key] = value;
        }

        var get = function (key) {
            return sharedKvPair[key];
        }

        var isKeyPresent = function (key) {
            return (sharedKvPair[key] != undefined);
        }

        var getDeepCopy = function () {
            return angular.copy(sharedKvPair);
        }

        var removeKey = function (key) {
            if (sharedKvPair[key] != undefined)
                delete sharedKvPair[key]
        }
        var removeItem = function (Key, ItemId) {
            if (isKeyPresent(Key)) {
                var index = _.findIndex(get(Key), function (item) { return item.ItemId == ItemId })
                if (index > -1) {
                    sharedKvPair[Key].splice(index, 1);
                }
            }
        }
        return {
            setValue: set,
            getValue: get,
            isKeyPresent: isKeyPresent,
            //Please dont call this function frequently. For a huge shared context, deep copying the data will affect performance.
            getSharedValues: getDeepCopy,
            removeKey: removeKey,
            removeItem: removeItem
        };
    }

    module.factory("sharedContext", sharedContext);

}(angular.module("common")));