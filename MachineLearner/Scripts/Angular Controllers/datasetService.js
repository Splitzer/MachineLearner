// machineRepository.js
(function () {
    'use strict';
    "ngInject";
    var machineRepository = function ($http, $q, baseUrl) {
        var machineList = [];
        var getList = function () {
            var deferred = $q.defer();
            $http.post(baseUrl + 'Dataset/GetDataset')
                .then(function (result) {
                    // Successful
                    angular.copy(result.data, machineList);
                    deferred.resolve();
                },
                    function (result) {
                        // Error
                        deferred.reject(result);
                    });
            return deferred.promise;
        };
        var getMachine = function (id) {
            var deferred = $q.defer();
            $http.post(baseUrl + 'Dataset/GetTuple' + id.trim())
                .success(deferred.resolve)
                .error(deferred.reject);
            return deferred.promise;
        }
        var filterMachines = function (filterCriteria) {
            if (filterCriteria != null) {
                filterCriteria.filters = [];
                for (var key in filterCriteria.fieldFilters) {
                    var obj = filterCriteria.fieldFilters[key];
                    var fieldFilter = { 'fieldName': key, 'filterValue': obj };
                    filterCriteria.filters.push(fieldFilter);
                }
            } else {
                filterCriteria = {};
            }
            var deferred = $q.defer();
            $http.post(baseUrl + 'Dataset/GetDataset', filterCriteria)
                .then(function (result) {
                    // Successful
                    deferred.resolve(result.data);
                },
                    function (result) {
                        // Error
                        deferred.reject(result);
                    });
            return deferred.promise;
        };
        return {
            tupleList: tupleList,
            getList: getList,
            getTuple: getTuple,
            saveTuple: saveTuple,
            filterDataset: filterDataset
        };
    };
    datasetService.$inject = ["$http", "$q", "baseUrl"];

    var module = angular.module('DatasetVisualiser'); //MachineLearner?
    module.factory('datasetService', datasetService);
})();