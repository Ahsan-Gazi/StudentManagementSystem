app.service('myService', function ($http) {

    this.semseterList = function () {
        var response = $http.get('Semester/SemesterList');
        return response;
    };

    this.addSemester = function (semester) {
        //alert(semester);
        var response = $http({
            method: 'post',
            url: 'Semester/AddSemester',
            data: JSON.stringify(semester)
        });
        return response;
    };

    this.updateSemester = function (semester) {
        //alert(product);
        var response = $http({
            method: 'post',
            url: 'Semester/UpdateSemester',
            data: JSON.stringify(semester),
        });
        return response;
    };

    this.deleteSemester = function (id) {
        var response = $http({
            method: 'post',
            url: 'Semester/DeleteSemester',
            params: { Id: JSON.stringify(id) }
        });
        return response;
    };

});
