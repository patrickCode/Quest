(function (module) {

    var proxy = function ($http) {
        var request = function (url, method, headers, data) {
            if (method === undefined || method === null)
                method = "GET";

            if (headers == undefined || headers === null)
                return $http({
                    method: method,
                    url: url,
                    data: data
                });

            return $http({
                method: method,
                url: url,
                data: data,
                headers: headers
            });
        }

        this.get = function (url, headers) {
            return request(url, "GET", headers);
        }

        this.post = function (url, data, headers) {
            return request(url, "POST", headers, data);
        }

        this.put = function (url, data, headers) {
            return request(url, "PUT", headers, data);
        }

        this.del = function (url, headers) {
            return request(url, "DELETE", headers);
        }

        this.patch = function (url, data, headers) {
            return request(url, "PATCH", headers, data);
        }
    }

    module.service("proxy", proxy);

}(angular.module("common")))