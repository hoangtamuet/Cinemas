app.controller('OrderController', function ($scope, $http) {
    // Khởi tạo mảng rỗng
    $scope.OrderEntities = [];
    $scope.ShowtimeEntities = [];
    // Khởi tạo một object tạm thời có tác dụng như dictionary
    $scope.OrderStorage = {};
    // Lấy dữ liệu từ trên server về browser
    $http.get('api/Showtimes').then(function (res) {
        $scope.ShowtimeEntities = res.data;
    }).catch(function (res) {
    });

    $http.get('api/Orders').then(function (res) {
        $scope.OrderEntities = res.data;
        // Server ko có IsEdit, IsEdit dùng để chuyển giữa 2 trạng thái Hiển thị và Tương tác, Hiển thị là false
        for (let i = 0; i < $scope.OrderEntities.length; i++)
            $scope.OrderEntities[i].IsEdit = false;
    }).catch(function (res) {
    });

    $http.get('api/Seats').then(function (res) {
        $scope.SeatEntities = res.data;
        // Server ko có IsEdit, IsEdit dùng để chuyển giữa 2 trạng thái Hiển thị và Tương tác, Hiển thị là false
        for (let i = 0; i < $scope.SeatEntities.length; i++)
            $scope.SeatEntities[i].IsEdit = false;
    }).catch(function (res) {
    });
    // Thêm Order trạng thái ban đầu là Tương tác, có lùi đầu dòng
    $scope.AddOrder = function () {
        let OrderEntity = { IsEdit: true };
        $scope.OrderEntities.unshift(OrderEntity);
    }
    // Sửa Order dựa trên index, ban đầu là Hiển thị, sau khi click mới là Tương tác
    $scope.EditOrder = function (index) {
        let OrderEntity = $scope.OrderEntities[index];
        $scope.OrderStorage[OrderEntity.Id] = angular.copy(OrderEntity);
        OrderEntity.IsEdit = true;
    }
    // Xóa Order dựa trên index, ban đầu là Hiển thị, 1 click là xóa 1 rạp
    $scope.DeleteOrder = function (index) {
        let OrderEntity = $scope.OrderEntities[index];
        $http.delete('api/Orders/' + OrderEntity.Id).then(function (res) {
            $scope.OrderEntities.splice(index, 1);
        }).catch(function (res) {
        })
    }
    // Lưu thay đổi dựa vào index có 2 trường hợp: Lưu Order mới và lưu Order đã có sẵn trong database
    $scope.SaveOrder = function (index) {
        let OrderEntity = $scope.OrderEntities[index];
        if (OrderEntity.Id === undefined) {
            $http.post('api/Orders', OrderEntity).then(function (res) {
                angular.copy(res.data, OrderEntity);
                OrderEntity.IsEdit = false;
            }).catch(function (res) {
            })
        }
        else {
            $http.put('api/Orders/' + OrderEntity.Id, OrderEntity).then(function (res) {
                // copy từng trường 
                angular.copy(res.data, OrderEntity);
                OrderEntity.IsEdit = false;
            }).catch(function (res) {
            });
        }
    }
    // Không sửa nữa mà khôi phục trạng thái ban đầu 
    $scope.CancelOrder = function (index) {
        let OrderEntity = $scope.OrderEntities[index];
        angular.copy($scope.OrderStorage[OrderEntity.Id], OrderEntity);
    }
    $scope.OrderEntity = null;
    $scope.SelectOrder = function (OrderEntity) {
        $scope.OrderEntity = OrderEntity;
    }
    $scope.AddSeat = function () {
        let SeatEntity = { IsEdit: true };
        $scope.OrderEntity.SeatEntities.unshift(SeatEntity);
    }

    $scope.SaveSeat = function (index) {
        let SeatEntity = $scope.OrderEntity.SeatEntities[index];
        $http.put('api/Orders/' + $scope.OrderEntity.Id + '/Seats/' + SeatEntity.Id).then(function (res) {
            if (res.data === "false") {
                $scope.OrderEntity.SeatEntities.splice(index, 1);
            }
            else {
                SeatEntity.IsEdit = false;
            }
        }).catch(function (res) { });
    }


    $scope.DeleteSeat = function (index) {
        let SeatEntity = $scope.OrderEntity.SeatEntities[index];
        $http.delete('api/Orders/' + $scope.OrderEntity.Id + '/Seats/' + SeatEntity.Id).then(function (res) {
            if (res.data === "true") {
                $scope.OrderEntity.SeatEntities.splice(index, 1);
            }
        }).catch(function (res) { });
    }
    $scope.SelectSeat = function (SeatEntity) {
        for (let i = 0; i < $scope.SeatEntities.length; i++) {
            if (SeatEntity.Id == $scope.SeatEntities[i].Id) {
                SeatEntity.Name = $scope.SeatEntities[i].Name;
                SeatEntity.RoomEntity = $scope.SeatEntities[i].RoomEntity;
            }
        }
    }
});