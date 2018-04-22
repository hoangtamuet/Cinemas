
app.controller('RoomController', function ($scope, $http) { // Định nghĩa controller và các dịch vụ được sử dụng trong controller
    // Khởi tạo một mảng rỗng 
    $scope.RoomEntities = [];
    $scope.CineplexEntities = [];

    // Tạo biến tạm thời là một object có tác dụng như một dictionary
    $scope.RoomStorage = {};

    // Lấy dữ liệu từ server về browser
    $http.get('api/Cineplexes').then(function (res) {
        $scope.CineplexEntities = res.data;
    }).catch(function (res) {
    });

    $http.get('api/Rooms').then(function (res) {
        $scope.RoomEntities = res.data;
        // Server ko có IsEdit, IsEdit dùng để chuyển giữa 2 trạng thái Hiển thị và Tương tác, Hiển thị là false
        for (let i = 0; i < $scope.RoomEntities.length; i++)
            $scope.RoomEntities[i].IsEdit = false;
    }).catch(function (res) {
    });

    // Thêm Room
    $scope.AddRoom = function () {
        let RoomEntity = { IsEdit: true };
        // unshift là thêm một biến vào đầu mảng RoomEntities
        $scope.RoomEntities.unshift(RoomEntity);
    }

    // Sửa Room ban đầu ở trạng thái Hiển thị, sau khi click mới chuyển về trạng thái tương tác
    $scope.EditRoom = function (index) {
        let RoomEntity = $scope.RoomEntities[index];
        // Dictionary: Sửa nhiều dòng đồng thời mà khi cancel có thể quay lại trạng thái ban đầu 
        // angular copy là tạo một object mới
        $scope.RoomStorage[RoomEntity.Id] = angular.copy(RoomEntity);
        // Sau khi click thì chuyển về Tương tác
        RoomEntity.IsEdit = true;
    }

    // Xóa Room ban đầu ở trạng thái Hiển thị, sau khi click thì xóa dòng Room đó dựa vào index
    $scope.DeleteRoom = function (index) {
        let RoomEntity = $scope.RoomEntities[index];
        $http.delete('api/Rooms/' + RoomEntity.Id).then(function (res) {
            $scope.RoomEntities.splice(index, 1); // splice là cắt một phần từ vị trí index, độ dài là 1;
            //nếu là splice(index, 2) thì giao diện mất 2 dòng nhưng data chỉ mất 1 
        }).catch(function (res) {
        })
    }

    // Lưu Room ban đầu ở trạng thái Tương tác, sau khi click thì dữ liệu thay đổi dựa vào index
    $scope.SaveRoom = function (index) {
        let RoomEntity = $scope.RoomEntities[index];
        // Lưu Room mới
        if (RoomEntity.Id === undefined) {
            $http.post('api/Rooms', RoomEntity).then(function (res) {
                angular.copy(res.data, RoomEntity);
                RoomEntity.IsEdit = false;
            }).catch(function (res) {
            })

        }
        // Lưu Room đang có trong database
        else {
            $http.put('api/Rooms/' + RoomEntity.Id, RoomEntity).then(function (res) {
                // copy từng trường 
                angular.copy(res.data, RoomEntity);
                RoomEntity.IsEdit = false;
            }).catch(function (res) {
            });
        }
    }
    // Không sửa nữa mà khôi phục trạng thái ban đầu
    $scope.CancelRoom = function (index) {
        let RoomEntity = $scope.RoomEntities[index];
        angular.copy($scope.RoomStorage[RoomEntity.Id], RoomEntity);
    }
});
