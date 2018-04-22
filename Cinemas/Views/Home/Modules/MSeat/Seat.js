app.controller('SeatController', function ($scope, $http) {
    $scope.SeatEntities = [];
    $scope.RoomEntities = [];

    $scope.SeatStorage = {};

    $http.get('api/Rooms').then(function (res) {
        $scope.RoomEntities = res.data;
    }).catch(function (res) {
    });

    //$http.get('api/Seats').then(function (res) {
    //    $scope.SeatEntities = res.data;
    //    for (let i = 0; i <= $scope.SeatEntities.length; i++)
    //        $scope.SeatEntities[i] = false;
    //}).catch(function (res) {
    //});

    //$scope.AddSeat = function () {
    //    let SeatEntity = { IsEdit: true };
    //    $scope.SeatEntities = unshift(SeatEntity);
    //}

});