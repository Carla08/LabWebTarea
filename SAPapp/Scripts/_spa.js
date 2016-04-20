var app = angular.module("spa", ["ngResource", "ngRoute"]);
app.controller("mainController", function ($scope) {

});

app.factory("employeesService", function ($resource) {
    //    $scope.employees = employeeService.query();
    return $resource("/api/Employees/:id",
        { id: "@id" },
        {
            update: { method: "PUT" }
        });
});

app.factory("departmentsService", function ($resource) {
    //    $scope.employees = employeeService.query();
    return $resource("/api/Departments/:id",
        { id: "@id" },
        {
            update: { method: "PUT" }
        });
});

app.controller("departmentsController", function ($scope, departmentsService) {
    $scope.departments = departmentsService.query();
    $scope.department = {
        Id : 0,
        Name : ''
    };
    $scope.deleteDepartment = function (department) {
        departmentsService.remove(department, $scope.refreshdata);
    };
    $scope.refreshdata = function () {
        $scope.departments = departmentsService.query();
    };
    $scope.showAddDialog = function () {
        $("#modal-dialog").modal("show");
    };
    $scope.saveDepartment = function () {
        departmentsService.save($scope.department, $scope.refreshdata);
        $("#modal-dialog").modal("hide");
        $scope.clearCurrentDepartment();
    };
    $scope.clearCurrentDepartment = function () {
        $scope.department = { Id: 0, Name: "" };
    };

});


app.controller("employeesController", function ($scope, employeesService, departmentsService) {
    $scope.departments = departmentsService.query();
    $scope.employees = employeesService.query();
    $scope.refreshdata = function () {
        $scope.employees = employeesService.query();
    };
    $scope.employee = {
        Id: 0,
        Name: '',
        DepartmentId: 0,
        Position: ""
    };
    $scope.deleteEmployee = function (employees) {
        employeesService.remove(employees, $scope.refreshdata);
    };
    $scope.showAddDialog = function () {
        $("#modal-dialog").modal("show");
    };
    $scope.saveEmployee = function () {
        employeesService.save($scope.employee, $scope.refreshdata);
        $("#modal-dialog").modal("hide");
        $scope.clearCurrentEmployee();
    };
    $scope.clearCurrentEmployee = function () {
        $scope.employee = { Id: 0, Name: "", Position: "", DepartmentId: 0};
    };
    
});

app.config(function ($routeProvider) {
    $routeProvider
    .when("/", {
        controller: "mainController"
    })
    .when("/employees", {
        templateUrl: "/Content/Views/Employees.html",
        controller: "employeesController"
    })
    .when("/departments", {
        templateUrl: "/Content/Views/Departments.html",
        controller: "departmentsController"
    });
});
