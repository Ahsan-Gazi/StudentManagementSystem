app.controller('myController', function ($scope, myService) {
    DepartmentList();


    $scope.newDepartment = {};
    $scope.clickedDepartment = {};
    $scope.message = "";

    function DepartmentList() {
        myService.departmentList().then(function (result) {
            $scope.Departments = result.data;
            console.log(result.data);
        });
    };

    $scope.addDepartment = function () {
        myService.addDepartment($scope.newDepartment)
            .then(function (result) {
                $scope.newDepartment = {};
                $scope.message = "Data saved successfully";
                DepartmentList()
                //console.log(result.data);
            });

        //, function (result) {
        //    alert(result)
        //}
        //);

    };

    $scope.selectedDepartment = function (department) {
        $scope.clickedDepartment = department;
    };

    $scope.updateDepartment = function () {


        myService.updateDepartment($scope.clickedDepartment).then(function (result) {
            $scope.message = "Data Update successfully";
            $scope.Departments = result;
            DepartmentList();
        }, function (result) {
            alert(result);
        });

    };

    $scope.deleteDepartment = function () {
        myService.deleteDepartment($scope.clickedDepartment.DepartmentId).then(function (result) {
            $scope.message = "Data Delete successfully";
            DepartmentList();
        }, function (result) {
            alert(result);
        });

    };

    $scope.clearInfo = function () {
        $scope.message = "";
    };

});

