
app.controller('FilmController', function ($scope, $http) { // Định nghĩa controller và các dịch vụ được sử dụng trong controller
    // Khởi tạo một mảng rỗng 
    $scope.FilmEntities = [];
    $scope.CategoryEntities = [];

    // Tạo biến tạm thời là một object có tác dụng như một dictionary
    $scope.FilmStorage = {};

    // Lấy dữ liệu từ server về browser
    $http.get('api/Categories').then(function (res) {
        $scope.CategoryEntities = res.data;
    }).catch(function (res) {
    });

    $http.get('api/Films').then(function (res) {
        $scope.FilmEntities = res.data;
        // Server ko có IsEdit, IsEdit dùng để chuyển giữa 2 trạng thái Hiển thị và Tương tác, Hiển thị là false
        for (let i = 0; i < $scope.FilmEntities.length; i++)
            $scope.FilmEntities[i].IsEdit = false;
        }).catch(function (res) {
        });

    // Thêm film
    $scope.AddFilm = function () {
        let FilmEntity = { IsEdit: true };
        // unshift là thêm một biến vào đầu mảng FilmEntities
        $scope.FilmEntities.unshift(FilmEntity);
    }

    // Sửa film ban đầu ở trạng thái Hiển thị, sau khi click mới chuyển về trạng thái tương tác
    $scope.EditFilm = function (index) {
        let FilmEntity = $scope.FilmEntities[index];
        // Dictionary: Sửa nhiều dòng đồng thời mà khi cancel có thể quay lại trạng thái ban đầu 
        // angular copy là tạo một object mới
        $scope.FilmStorage[FilmEntity.Id] = angular.copy(FilmEntity);
        // Sau khi click thì chuyển về Tương tác
        FilmEntity.IsEdit = true;
    }

    // Xóa film ban đầu ở trạng thái Hiển thị, sau khi click thì xóa dòng film đó dựa vào index
    $scope.DeleteFilm = function (index) {
        let FilmEntity = $scope.FilmEntities[index];
        $http.delete('api/Films/' + FilmEntity.Id).then(function (res) {
            $scope.FilmEntities.splice(index, 1); // splice là cắt một phần từ vị trí index, độ dài là 1;
            //nếu là splice(index, 2) thì giao diện mất 2 dòng nhưng data chỉ mất 1 
        }).catch(function (res) {
        })
    }

    // Lưu film ban đầu ở trạng thái Tương tác, sau khi click thì dữ liệu thay đổi dựa vào index
    $scope.SaveFilm = function (index) {
        let FilmEntity = $scope.FilmEntities[index];
        // Lưu film mới 
        if (FilmEntity.Id === undefined) {
            $http.post('api/Films', FilmEntity).then(function (res) {
                angular.copy(res.data, FilmEntity);
                FilmEntity.IsEdit = false;
            }).catch(function (res) {
            })
        
        }
        // Lưu film đang có trong database
        else {
            $http.put('api/Films/' + FilmEntity.Id, FilmEntity).then(function (res) {
                // copy từng trường 
                angular.copy(res.data, FilmEntity);
                FilmEntity.IsEdit = false;
            }).catch(function (res) {
            });
        }
    }
    // Không sửa nữa mà khôi phục trạng thái ban đầu
    $scope.CancelFilm = function (index) {
        let FilmEntity = $scope.FilmEntities[index];
        angular.copy($scope.FilmStorage[FilmEntity.Id], FilmEntity);
    }
});
