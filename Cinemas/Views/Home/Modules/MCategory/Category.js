app.controller('CategoryController', function ($scope, $http) { // Định nghĩa  controller và các dịch vụ được sử dụng trong controller
    $scope.SearchCategoryEntity = {};
    $scope.PageNumber = 0; // Số thứ tự trang 
    $scope.ItemNumber = 5; // Số lượng bản ghi cho một trang 
    $scope.PageTotal = 0; // Tổng số trang

    // Khởi tạo mảng rỗng
    $scope.CategoryEntities = [];
    $scope.CategoryCount = 0;

    // Khởi tạo một object tạm thời có tác dụng như dictionary
    $scope.CategoryStorage = {};
    // Lấy dữ liệu từ trên server về browser
    $scope.SearchCategory = function (index) {
        $scope.PageNumber = index === undefined ? 0 : index;
        // Skip: Tới bao nhiêu trang thì chuyển sang trang tiếp theo 
        $scope.SearchCategoryEntity.Skip = $scope.PageNumber * $scope.ItemNumber;
        // Take: Sau khi Skip thì tiếp tục Take bao nhiêu trang
        $scope.SearchCategoryEntity.Take = 5;
        $http({ url: 'api/Categories', method: 'GET', params: $scope.SearchCategoryEntity }).then(function (res) {
            $scope.CategoryEntities = res.data;
            // Server ko có IsEdit, IsEdit dùng để chuyển giữa 2 trạng thái Hiển thị và Tương tác, Hiển thị là false
            for (let i = 0; i < $scope.CategoryEntities.length; i++)
                $scope.CategoryEntities[i].IsEdit = false;
        }).catch(function (res) {
        });

        $http({ url: 'api/Categories/Count', method: 'GET', params: $scope.SearchCategoryEntity }).then(function (res) {
            $scope.CategoryCount = parseInt(res.data);
            if ($scope.CategoryCount % $scope.ItemNumber === 0)
                $scope.PageTotal = parseInt($scope.CategoryCount / $scope.ItemNumber);
            else
                $scope.PageTotal = parseInt($scope.CategoryCount / $scope.ItemNumber) + 1;
        }).catch(function (res) {
        });
    }
    $scope.SearchCategory();
    // Thêm Thể loại, trạng thái ban đầu là tương tác, sau click có lùi đầu dòng
    $scope.AddCategory = function () {
        let CategoryEntity = { IsEdit: true };
        $scope.CategoryEntities.unshift(CategoryEntity);
    }
    // Sửa Thể loại dựa trên index, ban đầu là Hiển thị, sau khi click mới là Tương tác
    $scope.EditCategory = function (index) {
        let CategoryEntity = $scope.CategoryEntities[index];
        $scope.CategoryStorage[CategoryEntity.Id] = angular.copy(CategoryEntity);
        CategoryEntity.IsEdit = true;
    }
    // Xóa Thê loại dựa trên index, ban đầu là Hiển thị, 1 click là xóa 1 Thể loại
    $scope.DeleteCategory = function (index) {
        let CategoryEntity = $scope.CategoryEntities[index];
        $http.delete('api/Categories/' + CategoryEntity.Id).then(function (res) {
            $scope.CategoryEntities.splice(index, 1);
        }).catch(function (res) {
        })
    }
    // Lưu thay đổi dựa vào index có 2 trường hợp: Lưu Thể loại mới và lưu Thể loại đã có sẵn trong database
    $scope.SaveCategory = function (index) {
        let CategoryEntity = $scope.CategoryEntities[index];
        if (CategoryEntity.Id === undefined) {
            $http.post('api/Categories', CategoryEntity).then(function (res) {
                angular.copy(res.data, CategoryEntity);
                CategoryEntity.IsEdit = false;
            }).catch(function (res) {
            })
        }
        else {
            $http.put('api/Categories/' + CategoryEntity.Id, CategoryEntity).then(function (res) {
                // copy từng trường 
                angular.copy(res.data, CategoryEntity);
                CategoryEntity.IsEdit = false;
            }).catch(function (res) {
            });
        }
    }
    // Không sửa nữa mà khôi phục trạng thái ban đầu 
    $scope.CancelCategory = function (index) {
        let CategoryEntity = $scope.CategoryEntities[index];
        angular.copy($scope.CategoryStorage[CategoryEntity.Id], CategoryEntity);
    }

});