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

    $scope.initUser = function () {
        $http({
            method: "POST",
            url: "/Admin/GetUsers",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.message == "") {
                vm.Users = data.data.users;
            }
            else {
                ErrorMessage(data.data.message);
            }
        });
    }

}])
