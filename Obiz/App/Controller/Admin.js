angular.module("admin", [])

.directive('onFinishRender', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                element.ready(function () {
                    // CALL TEST HERE!
                    $("#userTable").DataTable({
                        "searching": false,
                        "ordering": false,
                        "columnDefs":[
                            { "name": "username", "targets": 1 },
                            { "name": "name", "targets": 2 },
                            { "name": "department", "targets": 3}
                        ],
                        aLengthMenu: [5,10, 20, 30, 40, 50, 100]
                    });
                });
            }
        }
    }
})

.controller("adminController", ['$scope', '$location', '$http', 'growl',function ($scope, $location, $http, growl) {
    var vm = this;

    vm.pager = {};

    SuccessMessage = function(message){
        growl.success("Successfully " + message, { ttl:2000});
    }

    ErrorMessage = function(message){
        growl.error(message, {title: "Error!", ttl: 3000});
    }

    $scope.DepartmentDropDown = [
        { value: "AC", label: "Accounting" },
        { value: "AM", label: "Account Management" },
        { value: "BL", label: "Business & Leisure" },
        { value: "DC", label: "Documentation" },
        { value: "IT", label: "Information Technology" },
        { value: "MM", label: "Marine" },
        { value: "MI", label: "Mice" },
        { value: "MG", label: "Mancom" },
        { value: "EC", label: "ExCom" }
    ];

    $scope.DepartmentHeadDropDown = [
        { value: "Y", label: "Yes" },
        { value: "N", label: "No" }
    ];

    $scope.PriviledgeDropDown = [
        { value: "Y", label: "Yes" },
        { value: "N", label: "No" }
    ];

    $scope.initUser = function () {
        $http({
            method: "POST",
            url: "/Admin/GetUsers",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.message === "") {
                vm.Users = data.data.users;
            }
            else {
                ErrorMessage(data.data.message);
            }
        });
    }

    $scope.SaveUser = function (value) {
        $http({
            method: "POST",
            url: "/Admin/Save",
            data: { user: value }
        }).then(function (data) {
            if (data.data === "") {
                SuccessMessage("Sucessfully Saved");
            }
            else {
                ErrorMessage(data.data);
            }
        });
    }

    $scope.Redirection = function (ID) {
        window.location = "/Admin/OpenUserAccount?ID=" + ID;
    }

    $scope.GetSelectedUser = function () {
        var ID = "";

        if (window.location.href.indexOf('=') > 0) {
            ID = window.location.href.substr(window.location.href.indexOf('=') + 1, 36);
        }

        if (ID != "") {
            $http({
                method: "POST",
                url: "/Admin/GetSelectedUser",
                data: { ID: ID }
            }).then(function (data) {
                if (data.data.errorMessage === "") {
                    vm.UserAccount = data.data.user;
                }
                else {
                    ErrorMessage(data.data.errorMessage);
                }
            });
        }
    }
}])
