angular.module("aefur", [])

.directive("onFinishRenderUnbilled", function () {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                element.ready(function () {
                    // CALL TEST HERE!
                    $("#aefurUnbilledTable").DataTable({
                        searching: false,
                        ordering: false,
                    });

                });
            }
        }
    }
})

.directive("onFinishRenderNorecord", function () {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                element.ready(function () {
                    // CALL TEST HERE!
                    $("#aefurNoRecordTable").DataTable({
                        searching: false,
                        ordering: false,
                    });

                });
            }
        }
    }
})

.controller("aefurController", ['$scope', '$location', '$http', 'growl', function ($scope, $location, $http, growl) {
    var vm = this;

    vm.ForDelete = {};

    SuccessMessage = function (message) {
        growl.success("Successfully " + message, { ttl: 2000 });
    }


    ErrorMessage = function (message) {
        growl.error(message, { title: "Error!", ttl: 3000 });
    }

    $scope.initUnbilled = function () {
        vm.Unbilled = {};

        vm.NoRecord = {};

        $("#loading").show();

        $http({
            method: "POST",
            url: "/AEFUR/GetUnbilledList",
            arguments: { "Content-Type": "application/json" }
        }).then(function(data){
            if (data.data.errorMessage == "") {
                vm.Unbilled = data.data.unbilled;

                vm.NoRecord = data.data.noRecord;

                $("#loading").hide();

                $("#unbilledBox").show();

                $("#noRecordBox").show();
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        });
    }

    $scope.AssignForDelete = function (value) {
        vm.ForDelete = value;
    }

    $scope.AEFURTick = function () {
        $("#loading").show();

        $http({
            method: "POST",
            url: "/AEFUR/AEFURSubmit",
            data: { ticketNo: vm.ForDelete.TicketNumber }
        }).then(function (data) {
            $("#loading").hide();

            if (data.data == "") {
                vm.ForDelete.Status = "X";

                SuccessMessage("Successfully submitted ticket no:" + vm.ForDelete.TicketNumber);
            }
            else {
                ErrorMessage(data.data);
            }
        });
    }

    //REPORT MODULE
    $scope.initAEFURUnbilledReport = function () {
        $http({
            method: "POST",
            url: "/AEFUR/GetAEFURUnbilledReport",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.UnbilledReport = data.data.unbilled;

            //================BLD Charts===================
            var bldCTX = document.getElementById("BLDChart");

            var bldChart = new Chart(bldCTX, {
                type: 'horizontalBar',
                data: {
                    labels: vm.UnbilledReport.BLDUnbilled.TCName,
                    datasets: [{
                        label: '# of Unbilled',
                        data: vm.UnbilledReport.BLDUnbilled.Count,
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
                            'rgba(255, 159, 64, 0.2)',
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
            })

            //=================MARINE CHARTS==================
            var mmCTX = document.getElementById("MMChart");

            var mmChart = new Chart(mmCTX, {
                type: 'horizontalBar',
                data: {
                    labels: vm.UnbilledReport.MMUnbilled.TCName,
                    datasets: [{
                        label: '# of Unbilled',
                        data: vm.UnbilledReport.MMUnbilled.Count,
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
                            'rgba(255, 159, 64, 0.2)',
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
            })


        });
    }
}]);