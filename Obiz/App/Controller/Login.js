angular.module("login", ["angular-growl"])

.controller("loginController", ['$scope', '$location', '$http', 'growl', function ($scope, $location, $http, growl) {
    var vm = this;

    $scope.TryLogin = function (user) {
        $http({
            method: "POST",
            url: "/Home/Login",
            data: {
                username: user.Username,
                password: user.Password
            }
        }).then(function(data){
            if (data.data != "") {
                growl.error(data.data, { ttl: 3000 });
            }
            else {
                window.location.href = "/Home/Dashboard";
            }
        });
    }
}])