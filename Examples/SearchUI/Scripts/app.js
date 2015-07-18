(function () {
    var app = angular.module("searchApp", []);

    app.controller("SearchController", ["$http", SearchController]);

    function SearchController($http) {
        var ctrl = this;

        ctrl.request = {
            keyWord: ""
        };

        ctrl.result = {
            facets: {
            },
            documents: {
            },
            statistic: {
                timeToExecution: 0,
                documentCount: 0,
                pageCount: 0
            }
        };

        ctrl.search = function () {
            $http({
                url: "api/search",
                method: "GET",
                params: ctrl.request
            })
            .success(function (data, status) {
                console.log(data);

                ctrl.result = data;
            });
        };

        ctrl.search();
    }
}());