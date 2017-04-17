angular.module("serviceIssue", [])

.controller("serviceIssueController", ['$scope', '$location', '$http', 'growl',function ($scope, $location, $http, growl) {
    var vm = this;

    SuccessMessage = function (message) {
        growl.success("Successfully " + message, { ttl: 2000 });
    }

    ErrorMessage = function (message) {
        growl.error(message, { title: "Error!", ttl: 3000 });
    }

    $scope.Department = [
        { value: "IT", label: "Information Technology" },
        { value: "ACCTG", label: "Accounting" },
        { value: "HR", label: "Human Resource" },
        { value: "MM", label: "Marine" },
        { value: "AM", label: "Account Management" },
        { value: "BLD", label: "Business & Leisure" },
        { value: "DOCU", label: "Documentation" },
        { value: "EXCOM", label: "Excecutive" }
    ];

    $scope.Bool = [
        { value: "Y", label: "Yes" },
        { value: "N", label: "No"}
    ];

    getDropDown = function () {
        $http({
            method: "POST",
            url: "/ServiceIssueLog/GetDropdown",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.ClientDropDown = data.data.clientDropDown;

            vm.UserDropDown = data.data.userDropDown;

            vm.Priviledge = data.data.priviledge;

            vm.Temp = data.data.serviceIssue;

        });
    }

    openServiceIssue = function () {

        var temp = window.location.href.indexOf('=') + 1;

        $http({
            method: "POST",
            url: "/ServiceIssueLog/OpenServiceIssue",
            data: { ID: window.location.href.substr(temp , 36) }
        }).then(function (data) {
            vm.ServiceIssueLog = data.data.serviceIssue;

            if (vm.ServiceIssueLog.ShowReportDate != '') {
                vm.ServiceIssueLog.ReportDate = new Date(vm.ServiceIssueLog.ShowReportDate);
            }

            if (vm.ServiceIssueLog.ShowIncidentDate != '') {
                vm.ServiceIssueLog.IncidentDate = new Date(vm.ServiceIssueLog.ShowIncidentDate);
            }

            if (vm.ServiceIssueLog.ShowResolveDate != '') {
                vm.ServiceIssueLog.ResolveDate = new Date(vm.ServiceIssueLog.ShowReportDate);
            }
        });
    }

    $scope.initOpenServiceIssue = function () {
        getDropDown();
        
        if (window.location.href.indexOf('=') != -1) {
            openServiceIssue();
        }
        else {
            vm.ServiceIssueLog = vm.Temp;
        }
    }

    $scope.SaveServiceIssue = function (value) {
        $http({
            method: "POST",
            url: "/ServiceIssueLog/SaveServiceIssue",
            data: { logs: value }
        }).then(function (data) {
            if (data.data == "Saved" || data.data == "Updated") {
                SuccessMessage("Sucessfully Saved");
            }
            else {
                ErrorMessage(data.data);
            }
        });
    }

    $scope.Redirection= function (value) {
        window.location = '/ServiceIssueLog/OpenIssueLog?ID=' + value;
    }

    $scope.Init = function () {
        $http({
            method: "POST",
            url: "/ServiceIssueLog/GetServiceIssue",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            if (data.data.mesage == "") {
                vm.ServiceIssueLogs = data.data.list;

                vm.Priviledge = data.data.priviledge;

                vm.ClientDropDown = data.data.clientDropDown;

                vm.UserDropDown = data.data.userDropDown;
            }
            else {
                ErrorMessage(data.data.message);
            }
        });
    }


}])