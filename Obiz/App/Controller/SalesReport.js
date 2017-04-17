angular.module("salesReport", [])

    .directive('onFinishRenderSales', function () {
        return {
            restrict: 'A',
            link: function (scope, element, attr) {
                if (scope.$last === true) {
                    element.ready(function () {
                        // CALL TEST HERE!
                        $("#activityReportTable").DataTable({
                            searching: false,
                            ordering: false,
                            columnDefs: [
                            { "name": "username", "targets": 1 },
                            { "name": "name", "targets": 2 },
                            { "name": "department", "targets": 3 }
                            ],
                        });

                    });
                }
            }
        }
    })

.controller("salesReportController", ['$scope', '$location', '$http', 'growl' ,function ($scope, $location, $http, growl) {
    var vm = this;

    ClearVMSalesReport = function () {
        vm.SalesReport = {};

        vm.SalesReport.UserID = "";

        vm.SalesReport.ClientID = "";

        vm.SalesReport.TypeOfActivity = "";

        vm.SalesReport.ScopeAction = "";

        vm.SalesReport.AgendaIssueConcerns = "";

        vm.SalesReport.Date = "";

        vm.SalesReport.Category = "";

        vm.SalesReport.Status = "";

        vm.SalesReport.BCDDeliverables = "";

        vm.Search = {};
    }

    $scope.ClearModalFields = function () {
        ClearVMSalesReport();
    }

    $scope.MonthDropDown = [
        { value: "JAN", label: "January" },
        { value: "FEB", label: "February" },
        { value: "MAR", label: "March" },
        { value: "APR", label: "April" },
        { value: "MAY", label: "May" },
        { value: "JUN", label: "June" },
        { value: "JUL", label: "July" },
        { value: "AUG", label: "August" },
        { value: "SEP", label: "September" },
        { value: "OCT", label: "October" },
        { value: "NOV", label: "November" },
        { value: "DEC", label: "December" }
    ];

    $scope.TypeOfActivityDropDown = [
        { value: "I", label: "Business Review" },
        { value: "A", label: "Concall with BCD RAM or BCD GAM" },
        { value: "H", label: "ConCall with Client or Partner Supplier/Vendor" },
        { value: "B", label: "Coordination - Management - Adhoc Meetings" },
        { value: "M", label: "Escort/Familiarization Trips" },
        { value: "C", label: "Internal Training/Briefing with Servicing Team" },
        { value: "G", label: "Overseas Meetings" },
        { value: "E", label: "Preparation for Proposal - Business Review" },
        { value: "D", label: "Preparation for Travel Forums - Road Shows" },
        { value: "K", label: "Sales Visit - Meeting with Clients" },
        { value: "F", label: "Training (Product - Service - Techincal - Soft Skills" },
        { value: "J", label: "Travel Forum" },
        { value: "L", label: "Joint Sales Call with Partner Suppliers" },
    ];

    $scope.StatusDropDown = [
        { value: "Done", label: "Done" },
        { value: "In Progress", label: "In Progress" },
        { value: "Pending", label: "Pending" },
        { value: "To Be Determined", label: "To Be Determined" }
    ];

    $scope.CategoryDropDown = [
        { value: "Internal", label: "Internal" },
        { value: "External", label: "External" },
        { value: "Internal/External", label: "Internal / External" }
    ];

    SuccessMessage = function(message){
        growl.success("Successfully " + message, { ttl:2000});
    }

    ErrorMessage = function(message){
        growl.error(message, {title: "Error!", ttl: 3000});
    }

    $scope.Redirection = function (value) {
        window.location = "/SalesReport/SalesReport?ID=" + value;
    }

    openSalesReport = function () {
        var ID = "";

        if (window.location.href.indexOf('=') > 0) {
            ID = window.location.href.substr(window.location.href.indexOf('=') + 1, 36);
        }

        if (ID != "") {
            $http({
                method: "POST",
                url: "/SalesReport/OpenSalesReport",
                data: { ID: ID }
            }).then(function (data) {
                vm.SalesReport = data.data.salesReport;

                if (vm.SalesReport.ShowDate != '') {
                    vm.SalesReport.Date = new Date(vm.SalesReport.ShowDate);
                }

                if (vm.SalesReport.ShowDueDate != '') {
                    vm.SalesReport.DueDate = new Date(vm.SalesReport.ShowDueDate);
                }

                if (vm.SalesReport.SLA == "Y") {
                    $("#SLAYes").checked = true;
                }
                else {
                    $("#SLANo").checked = true;
                }

                vm.SalesReport.Priviledge = data.data.priviledge;
            });
        }
        else {
            $http({
                method: "POST",
                url: "/SalesReport/InitNewSalesReport",
                arguments: { "Content-Type": "application/json" }
            }).then(function (data) {
                vm.SalesReport = {};

                vm.SalesReport.Priviledge = data.data.priviledge;
            });
        }
    }

    getDropDown = function () {
        $http({
            method: "POST",
            url: "/SalesReport/GetDropdown",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.AccountManagerDropDown = data.data.AccountManagerDropdown;

            vm.ClientDropDown = data.data.ClientDropdown          
        });
    }

    $scope.initSalesReport = function () {
        getDropDown();

        openSalesReport();
    }

    $scope.ClearSearch = function () {
        vm.Search.Client = "";

        vm.Search.StartDate = null;

        vm.Search.EndDate = null;

        vm.Search.StartDeadline = null;

        vm.Search.EndDeadline = null;

        SuccessMessage("Search Parameter successfully cleard");
    }

    $scope.Search = function (value) {
        $http({
            method: "POST",
            url: "/SalesReport/GetSalesReport",
            arguments: { "Content-Type": "application/json" },
            data: {
                startDate: value.StartDate,
                endDate: value.EndDate,
                client: value.Client
            }
        }).then(function (data) {
            if (data.data.message == "") {
                vm.SalesHeader = data.data.list;

                vm.ShowSearch = {};

                vm.ShowSearch.ClientName = "[" + $("#searchClientName option:selected").text() + "]";

                vm.ShowSearch.StartDate = "[" + $("#searchStartDate").val();

                vm.ShowSearch.EndDate = " - " + $("#searchEndDate").val() + "]";
            }
            else {
                PopUpMessage(data.data.message);
            }
        });
    }

    $scope.SearchReport = function (value) {
        vm.AmProductivity = {};

        $http({
            method: "POST",
            url: " /SalesReport/GetReports",
            data: { startDate: value.StartDate,
                    endDate: value.EndDate
            },
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.AMProductivity = data.data.AMProductivity;

            var ctx = document.getElementById("piechart_3d");

            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: vm.AMProductivity.Names,
                    datasets: [{
                        label: '# Activities',
                        data: vm.AMProductivity.Productivities,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255,99,132,1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });
        });
    }

    $scope.initReport = function () {
        vm.AMProductivity = {};

        $http({
            method: "POST",
            url: " /SalesReport/GetReports",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.AMProductivity = data.data.AMProductivity;

            var ctx = document.getElementById("piechart_3d");

            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: vm.AMProductivity.Names,
                    datasets: [{
                        label: '# Activities',
                        data: vm.AMProductivity.Productivities,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255,99,132,1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });
        });
    }

    $scope.SearchTypeOfActivity = function (value) {
        vm.PercentageTypeActivity = {};

        $http({
            method: "POST",
            url: "/SalesReport/GetPercentageOfTypeOfActivity",
            data: {
                startDate: value.StartDate,
                endDate: value.EndDate},
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.PercentageTypeActivity = data.data.percentage;

            var ctx = document.getElementById("typeOfActivityChart");

            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: vm.PercentageTypeActivity.ShowTypeOfActivities,
                    datasets: [{
                        label: '# Type of Activity',
                        data: vm.PercentageTypeActivity.Count,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)',
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'

                        ],
                        borderColor: [
                            'rgba(255,99,132,1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)',
                            'rgba(255,99,132,1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)',
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });
        });
    }

    $scope.initReportPriv = function () {
        vm.Priviledge = {};

        $http({
            method: "POST",
            url: "/SalesReport/GetReportPriviledge",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.Priviledge = data.data;
        });
    }

    $scope.initTypeOfActivity = function () {
        vm.PercentageTypeActivity = {};

        $http({
            method: "POST",
            url: "/SalesReport/GetPercentageOfTypeOfActivity",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.PercentageTypeActivity = data.data.percentage;

            var ctx = document.getElementById("typeOfActivityChart");

            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: vm.PercentageTypeActivity.ShowTypeOfActivities,
                    datasets: [{
                        label: '# Type of Activity',
                        data: vm.PercentageTypeActivity.Count,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)',
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'

                        ],
                        borderColor: [
                            'rgba(255,99,132,1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)',
                            'rgba(255,99,132,1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)',
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });
        });
    }

    $scope.initTopClient = function () {
        $http({
            method: "POST",
            url: "/SalesReport/GetTop10Clients",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.TopClients = data.data.top;
        });
    }

    $scope.SearchTopClient = function (value) {
        $http({
            method: "POST",
            url: "/SalesReport/GetTop10Clients",
            data: {
                startDate: value.StartDate,
                endDate: value.EndDate
            }
        }).then(function (data) {
            vm.TopClients = data.data.top;
        });
    }

    $scope.init = function () {
        ClearVMSalesReport();

        getDropDown();

        $http({
            method: "POST",
            url: "/SalesReport/GetSalesReport",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.message == "") {
                vm.SalesHeader = data.data.list;

                vm.SalesHeader.Priviledge = data.data.priviledge;
            }
            else {
                PopUpMessage(data.data.message);
            }
        });
    }

    $scope.ViewSalesReport = function (value) {
        vm.SalesReport = value;

        if (vm.SalesReport.Date != null && vm.SalesReport.Date != "") {
            var datetimeInMilliseconds = parseInt(vm.SalesReport.Date.match(/\(([^)]+)\)/)[1]);
            var convertedDateTime = new Date(datetimeInMilliseconds);

            vm.SalesReport.Date = convertedDateTime;
        }

        if (vm.SalesReport.DueDate != null && vm.SalesReport.DueDate != "") {
            var datetimeInMilliseconds = parseInt(vm.SalesReport.DueDate.match(/\(([^)]+)\)/)[1]);
            var convertedDateTime = new Date(datetimeInMilliseconds);

            vm.SalesReport.DueDate = convertedDateTime;
        }
    }

    $scope.SaveSales = function (value) {
        var ifHasError = "N";

        if (value.UserID == "" || value.UserID == null) {
            ErrorMessage("Account Manager is required");

            ifHasError = "Y";
        }

        if (value.ClientID == "" || value.ClientID == null) {
            ErrorMessage("Client is required");

            ifHasError = "Y";
        }

        if (value.TypeOfActivity == "" || value.TypeOfActivity == null) {
            ErrorMessage("Type of Activity is required");

            ifHasError = "Y";
        }

        if (value.ScopeAction == "" || value.ScopeAction == null) {
            ErrorMessage("Scope / Action is required");

            ifHasError = "Y";
        }

        if (value.AgendaIssueConcerns == "" || value.AgendaIssueConcerns == null) {
            ErrorMessage("Agenda / Issue / Concern is required");

            ifHasError = "Y";
        }

        if (value.Date == "" || value.Date == null) {
            ErrorMessage("Date is required");

            ifHasError = "Y";
        }

        if (value.BCDDeliverables != "" && value.BCDDeliverables != null) {
            if (value.DueDate == "" || value.DueDate == null) {
                ErrorMessage("Due Date is Required")

                ifHasError = "Y";
            }
        }

        if (ifHasError == "N") {
            $http({
                method: "POST",
                url: "/SalesReport/SaveSalesReport",
                data: { salesReport: value }
            }).then(function (data) {
                if (data.data == "Updated" || data.data == "Saved") {
                    SuccessMessage(data.data);
                }
            });
        }
    }
}])