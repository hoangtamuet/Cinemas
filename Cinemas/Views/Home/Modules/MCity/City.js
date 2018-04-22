app.controller('CityController', function ($scope, $http) { // Định nghĩa  controller và các dịch vụ được sử dụng trong controller
    // Khởi tạo mảng rỗng
    $scope.CityEntities = [];

    // Khởi tạo một object tạm thời có tác dụng như dictionary
    $scope.CityStorage = {};
    // Lấy dữ liệu từ trên server về browser
    $http.get('api/Cities').then(function (res) {
        $scope.CityEntities = res.data;
        // Server ko có IsEdit, IsEdit dùng để chuyển giữa 2 trạng thái Hiển thị và Tương tác, Hiển thị là false
        for (let i = 0; i < $scope.CityEntities.length; i++)
            $scope.CityEntities[i].IsEdit = false;
    }).catch(function (res) {
    });
    // Thêm Thể loại, trạng thái ban đầu là tương tác, sau click có lùi đầu dòng
    $scope.AddCity = function () {
        let CityEntity = { IsEdit: true };
        $scope.CityEntities.unshift(CityEntity);
    }
    // Sửa Thể loại dựa trên index, ban đầu là Hiển thị, sau khi click mới là Tương tác
    $scope.EditCity = function (index) {
        let CityEntity = $scope.CityEntities[index];
        $scope.CityStorage[CityEntity.Id] = angular.copy(CityEntity);
        CityEntity.IsEdit = true;
    }
    // Xóa Thê loại dựa trên index, ban đầu là Hiển thị, 1 click là xóa 1 Thể loại
    $scope.DeleteCity = function (index) {
        let CityEntity = $scope.CityEntities[index];
        $http.delete('api/Cities/' + CityEntity.Id).then(function (res) {
            $scope.CityEntities.splice(index, 1);
        }).catch(function (res) {
        })
    }
    // Lưu thay đổi dựa vào index có 2 trường hợp: Lưu Thể loại mới và lưu Thể loại đã có sẵn trong database
    $scope.SaveCity = function (index) {
        let CityEntity = $scope.CityEntities[index];
        if (CityEntity.Id === undefined) {
            $http.post('api/Cities', CityEntity).then(function (res) {
                angular.copy(res.data, CityEntity);
                CityEntity.IsEdit = false;
            }).catch(function (res) {
            })
        }
        else {
            $http.put('api/Cities/' + CityEntity.Id, CityEntity).then(function (res) {
                // copy từng trường 
                angular.copy(res.data, CityEntity);
                CityEntity.IsEdit = false;
            }).catch(function (res) {
            });
        }
    }
    // Không sửa nữa mà khôi phục trạng thái ban đầu 
    $scope.CancelCity = function (index) {
        let CityEntity = $scope.CityEntities[index];
        angular.copy($scope.CityStorage[CityEntity.Id], CityEntity);
    }

});