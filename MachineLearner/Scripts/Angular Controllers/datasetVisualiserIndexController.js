// datasetVisualiserIndexController.js
(function () {
    'use strict';

    var datasetVisualiserIndexController = function ($scope, $modal, $q, $http, $location, $window, $timeout, dialogs, model, exportCsv, dialogInfo, gridHeaderSetRepository, machineRepository, customModals, baseUrl, popoverTemplate, popoverTemplateGrid) {
        $scope.headers = [
            {
                title: 'Machine Number',
                value: 'machineNumber',
                visible: true,
                anchor: true,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 180,
                type: 'none'
            },
            {
                title: 'Description',
                value: 'descr',
                visible: true,
                anchor: true,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 200,
                type: 'none'
            },
            {
                title: 'Alias',
                value: 'alias',
                visible: true,
                anchor: true,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 140,
                type: 'none'
            },
            {
                title: 'Machine Group',
                value: 'machineGroup',
                visible: true,
                anchor: true,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 140,
                type: 'none'
            },
            {
                title: 'Machine Enabled',
                value: 'machineEnable',
                visible: true,
                anchor: false,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'center',
                width: 140,
                type: 'bool'
            },
            {
                title: 'Date Range Enabled',
                value: 'dateRangeEnable',
                visible: false,
                anchor: false,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'center',
                width: 140,
                type: 'bool'
            },
            {
                title: 'Date Range End',
                value: 'dateRangeEnd',
                visible: false,
                anchor: false,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 140,
                type: 'datetime'
            },
            {
                title: 'Date Range Start',
                value: 'dateRangeStart',
                visible: false,
                anchor: false,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 140,
                type: 'datetime'
            },
            {
                title: 'Created By',
                value: 'createdBy',
                visible: true,
                anchor: false,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 170,
                type: 'none'
            },
            {
                title: 'Date Created',
                value: 'dateCreated',
                visible: true,
                anchor: false,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 180,
                type: 'datetime'
            },
            {
                title: 'Last Modified By',
                value: 'lastModifiedBy',
                visible: false,
                anchor: false,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 140,
                type: 'none'
            },
            {
                title: 'Date Last Modified',
                value: 'dateLastModified',
                visible: true,
                anchor: false,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 180,
                type: 'datetime'
            },
            {
                title: 'Purchase Date',
                value: 'purchaseDate',
                visible: false,
                anchor: false,
                enableGrouping: false,
                groupedOn: false,
                input: 'none',
                alignment: 'left',
                width: 140,
                type: 'datetime'
            }
        ];

        gridController.call(this, $scope, model, dialogs, gridHeaderSetRepository, dialogInfo);

        // CSV Export
        $scope.csvFileName = "PortalMVCMachines.csv";
        $scope.getCsvDataAll = function () {
            exportCsv.getCsvData($scope.machines, $scope.headers);
            $scope.csvHeaders = exportCsv.csvHeaders;
            var deferred = $q.defer();
            $scope.filterCriteria.isCsvExport = true;
            $http.post(baseUrl + "Machines/GetMachines", { filterCriteria: $scope.filterCriteria, fieldSet: exportCsv.getFieldSetForCsvExport($scope.headers) })
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

        $scope.selectAllRows = function () {
            //check if all selected or not
            if ($scope.isAll === false) {
                //set all row selected
                angular.forEach($scope.machines, function (row, index) {
                    $scope.tableSelection.push({ machineNumber: $scope.machines[index].machineNumber, description: $scope.machines[index].descr });
                });
                $scope.isAll = true;
            } else {
                //set all row unselected
                $scope.tableSelection = [];
                $scope.isAll = false;
            }
        };

        $scope.selectTrue = function (machineNumber) {
            for (var i = 0; i < $scope.tableSelection.length; i++)
                if ($scope.tableSelection[i].machineNumber === machineNumber) {
                    return true;
                }
            return false;
        }

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

        // Save Table Columns State
        $scope.updateTableColumnsWidth = {
        };

        $scope.confirmGridReset = function () {
            $scope.deleteGridSettings("Machines");
        };

        $scope.updateTableColumnState = function () {
            var gridHeaderSet = {
                gridTable: { tableName: "Machines" }
            };
            $scope.updateGridSettings(gridHeaderSet);
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
        };

        $scope.totalPages = 0;
        $scope.totalItems = 0;

        $scope.fetchResult = function () {
            $scope.selectedMachineNumber = null;
            return machineRepository.filterMachines($scope.filterCriteria).then(function (data) {
                $scope.machines = data.machines;
                $scope.totalPages = Math.ceil(data.totalItems / $scope.filterCriteria.pageSize);
                $scope.totalItems = data.filterCriteria.totalItems;
                $scope.tableWidth = $scope.calculateTableWidth();
                angular.element("table").colResizable({ disable: true });
                angular.element("table").colResizable({});
            }, function () {
                $scope.machines = [];
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

        $scope.copyMachine = function (e) {
            $('#page-container').block({ message: "Loading..." });
            var machineNumber = e.target.getAttribute('data-value');
            window.location = "/Machines/Copy/" + machineNumber;
        };

        $scope.editMachine = function (e) {
            $('#page-container').block({ message: "Loading..." });
            var machineNumber = e.target.getAttribute('data-value');
            window.location = "/Machines/Edit/" + machineNumber;
        };

        function createListFromMachineNumbers(obj) {
            var str = '<ul class="xbreadcrumbs">';
            for (var i in obj) {
                str += '<li><a id="' + obj[i].machineNumber + '" href="/Machines/Edit/' + obj[i].machineNumber + '">' + obj[i].description + ' (' + obj[i].machineNumber + ')</a><input type="hidden" name="hdnMachineNumber' + i + '" value="' + obj[i].machineNumber + '"</li>';
            }
            str += '</ul>';
            return str;
        }

        var modalTemplate = "";
        var id = "";

        $scope.editMultipleMachines = function () {
            if (!isEmpty($scope.tableSelection)) {
                var str = createListFromMachineNumbers($scope.tableSelection);
                $(".modal-body #machineNumbers").html(str);
                modalTemplate = $('#modal-content').html(); // make a copy of empty modal to reset form
                $('#editMultipleMachinesModal').modal('show');
            }
            else
                dialogInfo.showDialog("Please select the machines you wish to edit.", "error");
        }

        $scope.editMultipleVendingMachineAssignments = function () {
            if (!isEmpty($scope.tableSelection)) {
                var str = createListFromMachineNumbers($scope.tableSelection);
                $(".modal-body #VMAmachineNumbers").html(str);
                if ($scope.tableSelection.length === 1) {
                    id = $scope.tableSelection[0].machineNumber;
                    checkBoxListButtonPress('location-assignments', false, id, 'LocationAssignmentsLoading', '/machines/CheckBoxListOps');
                    $('#editVendingMachinesModal').modal('show');
                }
                else {
                    id = null;
                    checkBoxListButtonPress('location-assignments', false, null, 'LocationAssignmentsLoading', '/machines/CheckBoxListOps');
                    $('#editVendingMachinesModal').modal('show');
                }
            }
            else
                dialogInfo.showDialog("Please select the machines you wish to edit.", "error");
        };

        $('#editMultipleMachinesModal').on('hidden.bs.modal', function () {
            $('#modal-content').html(modalTemplate);
        });

        $('#editVendingMachinesModal').on('hidden.bs.modal', function () {
            vmaArray = [];
        });

        $scope.showDeleteAuditModal = function () {
            $("#deleteHistoryModal").modal('show');
        }

        $(document).on("click", "#btnDeleteMachines", function (e) {
            $('#editMultipleMachinesModal').modal('hide');
            $scope.deleteMachines();
        });

        $(document).on("click", "#btnMultiMachineSave", function (e) {
            $.ajax({
                url: "Machines/EditMultipleMachineDetails",
                type: "post",
                data: $("form").serialize(),
                success: function (result) {
                    $('#editMultipleMachinesModal').modal('hide');
                    dialogInfo.showDialog("Records updated successfully.", "success");
                    $scope.fetchResult();
                },
                // failed
                error: function () {
                    $('#editMultipleMachinesModal').modal('hide');
                    dialogInfo.showDialog("Unable to edit selected records, the records may be in use. If the problem persists then please contact support.", 'error');
                }
            });
        });

        $(document).on("click", "#btnMultiLocationAssignmentsSave", function (e) {
            $.ajax({
                url: "Machines/EditMultipleLocationAssignments",
                type: "post",
                data: $("form").serialize(),
                success: function (result) {
                    $('#editVendingMachinesModal').modal('hide');
                    dialogInfo.showDialog("Records updated successfully.", "success");
                    $scope.fetchResult();
                },
                // failed
                error: function () {
                    $('#editVendingMachinesModal').modal('hide');
                    dialogInfo.showDialog("Unable to edit selected records, the records may be in use. If the problem persists then please contact support.", 'error');
                }
            });
        });

        // Delete machines
        $scope.deleteMachines = function () {
            if (!isEmpty($scope.tableSelection)) {
                var str = createListFromMachineNumbers($scope.tableSelection);
                var dlg = dialogs.confirm("Please Confirm", "Are you sure that you want to delete the following machines?" + str, { size: "sm", keyboard: true, backdrop: false, windowClass: "my-class" });
                dlg.result.then(function (btn) {
                    // Delete from database
                    machineRepository.deleteMachines($scope.tableSelection).then(
                        // success
                        function () {
                            $scope.fetchResult();
                            $('#editMultipleMachinesModal').modal('hide');
                            $scope.tableSelection = [];
                            dialogInfo.showDialog("Records deleted successfully.", "success");
                        },
                        // failed
                        function () {
                            dialogInfo.showDialog("Unable to delete machines, records may be in use. If the error persists please contact support.", "error");
                        });
                }, function (btn) {
                    // Just ignore
                });
            }
            else {
                dialogInfo.showDialog("Please select the machines you wish to delete.", "error");
            }
        };
    }
    //datasetVisualiserIndexController.$inject = ["$scope", "$modal", "$location", "$window", "$timeout", "$http", "dialogs", "model", "exportCsv", "dialogInfo", "gridHeaderSetRepository", "machineRepository", "customModals", "baseUrl", "popoverTemplate", "popoverTemplateGrid"];
    var module = angular.module('PortalMVCApp');
    angular.module('PortalMVCApp').config(function ($sceProvider) {
        $sceProvider.enabled(false);
    });
    module.controller("datasetVisualiserIndexController", datasetVisualiserIndexController);
})();