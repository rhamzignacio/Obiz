var app = angular.module("app", ["angular-growl","login", "salesReport", "admin", "serviceIssue", "aefur"])

.controller("mainController", ['$scope', '$location', '$http', 'growl', function ($scope, $location, $http, growl) {
    var vm = this;

    PopUpMessage = function (message) {
        if (message == "Saved" || message == "Updated") {
            growl.success("Successfully " + message, { ttl: 2000 });
        }
        else {
            growl.error(message, { title: "Error!", ttl: 3000 });
        }
    }

    $scope.init = function () {
        $http({
            method: "POST",
            url: "/Home/GetCurrentUser",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            vm.CurrentUser = data.data.user;

            vm.Menu = data.data.menu;
        });
    }

    $scope.Logout = function () {
        $http({
            method: "POST",
            url: "/Home/Logout",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            if (data.data != "") {
                PopUpMessage(data.data);
            }
            else {
                window.location.href = "/Home/Login";
            }
        });
    }
}]);