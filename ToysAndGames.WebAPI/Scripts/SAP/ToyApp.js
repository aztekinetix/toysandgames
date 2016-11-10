
var toyApp = angular.module("toyApp", ['ngRoute']);
toyApp.config(function ($routeProvider)
{
    $routeProvider
        .when('/', {
            controller: 'ToyController',
            templateUrl: 'NgPartialsViews/List.html'
        })
        .when('/Edit/:id', {
            controller: 'ToyEditController',
            templateUrl: 'NgPartialsViews/Edit.html'

        })
        .when('/Add/', {
            controller: 'ToyEditController',
            templateUrl: 'NgPartialsViews/Edit.html'

        })
        .when('/Delete/:id', {
            controller: 'ToyDeleteController',
            templateUrl: 'NgPartialsViews/Delete.html'

        })
        .otherwise({ redirectTo: '/' });
});
var controllers = {};
controllers.ToyController = function ($scope,$http)
{
    init();
        
    //getting all the Toys from the webApi
    function init ()
    {
        $http.get('api/Toy').success(function (dataFromWs)
        {
            $scope.toySet = dataFromWs;
        }).error(function ()
        {
            $scope.error = "Error loading the information from the source";

        });
    };

    $scope.delete = function (id,name)
    {
        $scope.idToDelete = id;
        $scope.nameToDelete = name;
        deleteUser = window.confirm('Are you sure you want to delete '+$scope.nameToDelete + '?');
        if (deleteUser)
        {
            $http.delete('api/Toy/' + $scope.idToDelete).success(function () { init();}).error(
                function (data)
                {
                    $scope.error = "Error loading the information from the source " +data.ExceptionMessage ;

                }
                );
            
            
        }
    };
};

controllers.DeleteToyController = function ($scope, $http, $routeParams, $location)
{
    //Model popup events
    
};

controllers.ToyEditController = function ($scope,$http,$routeParams,$location)
{

    $scope.agesOptions = function (start,end)
    {
       
            var result = [];
            for (var i = start; i <= end; i++)
            {
                result.push(i);
            }
            return result;
        
    };

    //getting the Toys from the webApi
    $scope.id = 0;

    if ($routeParams.id) //if the value comes from the route :id
    {
        $scope.id = $routeParams.id;
        $scope.title = "Edit Toy";

        $http.get('api/Toy/' + $routeParams.id)
            .success(function (dataFromWs)
            {
                var toyToEdit = dataFromWs;
                $scope.Name = toyToEdit.Name;
                $scope.Price = toyToEdit.Price;
                $scope.Description = toyToEdit.Description;
                $scope.Company = toyToEdit.Company;
                $scope.AgeRestriction = toyToEdit.AgeRestriction;
                })
            .error(function (data)
        {
            $scope.error = "Error loading the Toy information from the source " +data.ExceptionMessage ;
           
        });

    }
    else
    {
        $scope.title = "Add new toy";
        $scope.Name ="";
        $scope.Price ="";
        $scope.Description ="";
        $scope.Company = "";
        $scope.AgeRestriction =""
    }
    $scope.save = function ()
    {
        var toyToSave = {
            Id: $scope.id,
            Name: $scope.Name,
            Description: $scope.Description,
            AgeRestriction:$scope.AgeRestriction,
            Price: $scope.Price,
            Company:$scope.Company
        }
        if ($scope.id == 0) {

 

            $http.post('/api/Toy/', toyToSave).success(function (data) {

                $location.path('/');

            }).error(function (data) {

                $scope.error = "An error has occured while adding a Toy " + data.Message;

            });

        }

        else {

 

            $http.put('/api/Toy/', toyToSave).success(function (data) {

                $location.path('/');

            }).error(function (data) {

                console.log(data);

                $scope.error = "An Error has occured while Saving customer! " + data.ExceptionMessage;

            });

        }
    }
}
toyApp.controller(controllers);

