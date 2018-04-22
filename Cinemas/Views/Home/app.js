var app = angular.module('CinemasApp', [])
app.controller('MainController', function ($scope, $http) {
    $scope.Tabs = [true, false, false, false];
});

app.controller('HeaderController', function ($scope, $http) {
    
    $scope.ChooseTab = function (index) {
        for (let i = 0; i < $scope.Tabs.length; i++)
            $scope.Tabs[i] = false;
        $scope.Tabs[index] = true;
    }
});

app.filter('range', function () {
    return function (input, total) {
        total = parseInt(total);

        for (var i = 0; i < total; i++) {
            input.push(i);
        }

        return input;
    };
});