// datasetVisualiserIndexController.js
(function () {
    'use strict';

    var datasetVisualiserIndexController = function ($scope, $modal, $q, $http, $location, $window, $timeout, dialogs, model, exportCsv, dialogInfo, gridHeaderSetRepository, dataRepository, customModals, baseUrl, popoverTemplate, popoverTemplateGrid) {

        $scope.headers[0] = [
            {
                title: 'Output - X',
                value: 'datasetOutput',
                visible: true,
                anchor: true,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 180,
                type: 'none'
            }

        ];

        for (var i = 1; i < dataRepository.columns.length; i++) {
            $scope.headers[i] = {
                title: "Column " + i,
                visible: true,
                anchor: true,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 180,
                type: 'none'
            };
        }

        gridController.call(this, $scope, model, dialogs, gridHeaderSetRepository, dialogInfo);

        // CSV Export
        $scope.csvFileName = "PortalMVCMachines.csv";
        $scope.getCsvDataAll = function () {
            exportCsv.getCsvData($scope.machines, $scope.headers);
            $scope.csvHeaders = exportCsv.csvHeaders;
            var deferred = $q.defer();
            $scope.filterCriteria.isCsvExport = true;
            $http.post(baseUrl + "Dataset/GetDataset", { filterCriteria: $scope.filterCriteria, fieldSet: exportCsv.getFieldSetForCsvExport($scope.headers) })
                .then(function (result) {
                    // Successful
                    $scope.filterCriteria.isCsvExport = false;
                    exportCsv.getCsvData(result.data.machines, $scope.headers);
                    deferred.resolve(exportCsv.csvRecords);
                    return deferred.promise
                        .then(function (data) {
                            return data;
                        });
                    },
                    function (result) {
                        // Error
                        $scope.filterCriteria.isCsvExport = false;
                        deferred.reject(result);
                    });
            return deferred.promise;
        };
        $scope.getCsvData = function () {
            exportCsv.getCsvData($scope.machines, $scope.headers);
            $scope.csvHeaders = exportCsv.csvHeaders;
            return exportCsv.csvRecords;
        }
        // End CSV Export
        $scope.tableSelection = [];
        $scope.headersSet = model;
        $scope.BooleanFilters = [{ name: "All", id: "" }, { name: "Yes", id: "true" }, { name: "No", id: "false" }];

        $scope.isAll = false;

        $scope.shouldBeDisabled = function (title) {
            if (title === 'Select') {
                return true;
            } else {
                return false;
            }
        };

        $scope.refreshGrid = function () {
            $scope.fetchResult();
            dialogInfo.showDialog('Grid data updated.', 'success');
        }

        $scope.showNotImplemented = function () {
            dialogInfo.showDialog('To be implemented.', 'error');
        }

        $scope.tableFirstColumnWidth = 85;

        var dialog;

        // Create Headers Array
        $scope.loadHeadersArray("machineNumber", "asc", 10);

        // Column hide/position popover template
        $scope.popoverTemplate = popoverTemplate;

        // Grid save/reset export to CSV grid settings popover template
        $scope.popoverTemplateGrid = popoverTemplateGrid;

        // Refresh Resizing grid on columns change position
        $scope.onItemsChange = function () {
            $scope.fetchResult();
        };

        // Toggle Columns visibility
        $scope.toggleColumnsVisibility = function (header) {
            header.visible = !header.visible;
            $scope.fetchResult();
        };

        // Get Visible Headers Count for RowSpan argument
        $scope.getVisibleHeadersCount = function () {
            var visbleHeaders = 1;
            for (var i = 0; i < $scope.headers.length; i++) {
                if ($scope.headers[i].visible) {
                    visbleHeaders += 1;
                }
            }
            return visbleHeaders;
        };

        $scope.toggleColumns = function (fieldName) {
            var column, i;
            for (i = 0; i < $scope.headers.length; i++) {
                column = $scope.headers[i];
                if (column.value == fieldName) {
                    return column.visible;
                }
            };
            return true;
        }; //???

        $scope.totalPages = 0;
        $scope.totalItems = 0;

        $scope.fetchResult = function () {
            $scope.selectedMachineNumber = null;
            return dataRepository.filterData($scope.filterCriteria).then(function (data) {
                $scope.dataset = data.dataset;
                $scope.totalPages = Math.ceil(data.totalItems / $scope.filterCriteria.pageSize);
                $scope.totalItems = data.filterCriteria.totalItems;
                $scope.tableWidth = $scope.calculateTableWidth();
                angular.element("table").colResizable({ disable: true });
                angular.element("table").colResizable({});
            }, function () {
                $scope.dataset = [];
                $scope.totalPages = 0;
                $scope.totalItems = 0;
            });
        };

        $scope.selectPage = function () {
            $scope.filterCriteria.pageNumber = $scope.filterCriteria.pageNumber;
            $scope.fetchResult();
        };

        $scope.pageSizeChanged = function (pageSize) {
            $scope.filterCriteria.pageSize = pageSize;
            $scope.filterCriteria.pageNumber = 1;
            $scope.fetchResult();
        };

        $scope.filterResult = function () {
            $scope.filterCriteria.pageNumber = 1;
            $scope.fetchResult().then(function () {
                $scope.filterCriteria.pageNumber = 1;
            });
        };

        $scope.onSort = function (sortedBy, sortDir) {
            $scope.filterCriteria.sortDir = sortDir;
            $scope.filterCriteria.sortedBy = sortedBy;
            $scope.filterCriteria.pageNumber = 1;
            $scope.fetchResult().then(function () {
                $scope.filterCriteria.pageNumber = 1;
            });
        };

        $scope.selectPage(1);
        $scope.user = {};
    }
    //datasetVisualiserIndexController.$inject = ["$scope", "$modal", "$location", "$window", "$timeout", "$http", "dialogs", "model", "exportCsv", "dialogInfo", "gridHeaderSetRepository", "machineRepository", "customModals", "baseUrl", "popoverTemplate", "popoverTemplateGrid"];
    var module = angular.module('DatasetVisualiser'); //MachineLearner?
    angular.module('DatasetVisualiser').config(function ($sceProvider) {
        $sceProvider.enabled(false);
    });
    module.controller("datasetVisualiserIndexController", datasetVisualiserIndexController);
})();