app.service('myService', function ($http) {

    this.departmentList = function () {
        var response = $http.get('Department/DepartmentList');
        return response;
    };

    this.addDepartment = function (department) {
        //alert(product);
        var response = $http({
            method: 'post',
            url: 'Department/AddDepartment',
            data: JSON.stringify(department)
        });
        return response;
    };

    this.updateDepartment = function (department) {
        //alert(product);
        var response = $http({
            method: 'post',
            url: 'Department/UpdateDepartment',
            data: JSON.stringify(department),
        });
        return response;
    };

    this.deleteDepartment = function (id) {
        var response = $http({
            method: 'post',
            url: 'Department/DeleteDepartment',
            params: { Id: JSON.stringify(id) }
        });
        return response;
    };

});
