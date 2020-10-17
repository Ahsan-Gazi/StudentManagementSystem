app.controller('myController', function ($scope, myService) {
    SemesterList();
  

    $scope.newSemester = {};
    $scope.clickedSemester = {};
    $scope.message = "";

    function SemesterList() {
        myService.semseterList().then(function (result) {
            $scope.Semesters = result.data;
            console.log(result.data);
        });
    };

    $scope.addSemester = function () {
        myService.addSemester($scope.newSemester)
            .then(function (result) {
                $scope.newSemester = {};
                $scope.message = "Data saved successfully";
                SemesterList()
                    //console.log(result.data);
        });

           //, function (result) {
        //    alert(result)
        //}
        //);
   
    };

    $scope.selectedSemester = function (semester) {
        $scope.clickedSemester = semester;
    };

    $scope.updateSemester = function () {


        myService.updateSemester($scope.clickedSemester).then(function (result) {
            $scope.message = "Data Update successfully";
            $scope.Semesters = result;
            SemesterList();
        }, function (result) {
            alert(result);
        });
      
    };

    $scope.deleteSemester = function () {
        myService.deleteSemester($scope.clickedSemester.SemesterId).then(function (result) {
            $scope.message = "Data Delete successfully";
            SemesterList();
        }, function (result) {
            alert(result);
        });

    };

    $scope.clearInfo = function () {
        $scope.message = "";
    };

});

