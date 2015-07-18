(function () {
    var app = angular.module("searchApp", []);

    app.controller("SearchController", ["$http", SearchController]);

    function SearchController($http) {
        var ctrl = this;

        ctrl.request = {
            page: 1,
            keyWord: ""
        };
        
        ctrl.result = {
            facets:
            {
                field: [],
                query: [],
                range: []
            },
            statistic:
            {
                timeToExecution: 0,
                documentCount: 0,
                pageCount: 1
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

                ctrl.request.page = 1;
            });
        };

        ctrl.pageRange = function () {
            var range = [];
            for (var i = ctrl.request.page; i <= ctrl.result.statistic.pageCount; i++) {
                range.push(i);
            }
            
            return range;
        }

        ctrl.canNavigate = function () {
            return ctrl.result.statistic.pageCount > 1;
        }

        ctrl.canNavigateToPreviousPage = function () {
            return ctrl.request.page > 1;
        }

        ctrl.canNavigateToNextPage = function () {
            return ctrl.request.page < ctrl.result.statistic.pageCount > 1;
        }

        ctrl.search();
    }
}());