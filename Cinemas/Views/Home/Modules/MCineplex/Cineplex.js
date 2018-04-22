app.controller('CineplexController', function ($scope, $http) { // Định nghĩa  controller và các dịch vụ được sử dụng trong controller
    // Khởi tạo mảng rỗng
    $scope.CineplexEntities = [];
    $scope.CityEntities = [];
    // Khởi tạo một object tạm thời có tác dụng như dictionary
    $scope.CineplexStorage = {};
    // Lấy dữ liệu từ trên server về browser
    $http.get('api/Cities').then(function (res) {
        $scope.CitiesEntities = res.data;
    }).catch(function (res) {
    });

    $http.get('api/Cineplexes').then(function (res) {
        $scope.CineplexEntities = res.data;
        // Server ko có IsEdit, IsEdit dùng để chuyển giữa 2 trạng thái Hiển thị và Tương tác, Hiển thị là false
        for (let i = 0; i < $scope.CineplexEntities.length; i++)
            $scope.CineplexEntities[i].IsEdit = false;
    }).catch(function (res) {
    });
    // Thêm Rạp trạng thái ban đầu là Tương tác, có lùi đầu dòng
    $scope.AddCineplex = function () {
        let CineplexEntity = { IsEdit: true };
        $scope.CineplexEntities.unshift(CineplexEntity);
    }
    // Sửa Rạp dựa trên index, ban đầu là Hiển thị, sau khi click mới là Tương tác
    $scope.EditCineplex = function (index) {
        let CineplexEntity = $scope.CineplexEntities[index];
        $scope.CineplexStorage[CineplexEntity.Id] = angular.copy(CineplexEntity);
        CineplexEntity.IsEdit = true;
    }
    // Xóa Rạp dựa trên index, ban đầu là Hiển thị, 1 click là xóa 1 rạp
    $scope.DeleteCineplex = function (index) {
        let CineplexEntity = $scope.CineplexEntities[index];
        $http.delete('api/Cineplexes/' + CineplexEntity.Id).then(function (res) {
            $scope.CineplexEntities.splice(index, 1); 
        }).catch(function (res) {
        })
    }
    // Lưu thay đổi dựa vào index có 2 trường hợp: Lưu Rạp mới và lưu rạp đã có sẵn trong database
    $scope.SaveCineplex = function (index) {
        let CineplexEntity = $scope.CineplexEntities[index];
        if (CineplexEntity.Id === undefined) {
            $http.post('api/Cineplexes', CineplexEntity).then(function (res) {
                angular.copy(res.data, CineplexEntity);
                CineplexEntity.IsEdit = false;
            }).catch(function (res) {
            })
        }
        else {
            $http.put('api/Cineplexes/' + CineplexEntity.Id, CineplexEntity).then(function (res) {
                // copy từng trường 
                angular.copy(res.data, CineplexEntity);
                CineplexEntity.IsEdit = false;
            }).catch(function (res) {
            });
        }
    }
    // Không sửa nữa mà khôi phục trạng thái ban đầu 
    $scope.CancelCineplex = function (index) {
        let CineplexEntity = $scope.CineplexEntities[index];
        angular.copy($scope.CineplexStorage[CineplexEntity.Id], CineplexEntity);
    }

});