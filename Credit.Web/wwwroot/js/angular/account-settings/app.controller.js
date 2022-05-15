
angular.module("appAccountSettings")
    .controller('AccountSettingsController', 
        function($scope,$http,$httpParamSerializerJQLike) {
            $scope.menuSelected = "user";
            $scope.menuClick = function(selected){
                if(selected != 'password') {
                    $scope.responseMessageDisplayed = $scope.responseMessages.none;
                    $scope.passwords={};
                }       
                $scope.menuSelected=selected;
                animateElements($scope.menuSelected);
            };

            $scope.passwords = {};
            $scope.newPwdsNotMatch=false;
            $scope.newPwdsCheck=function(){
                $scope.newPwdsNotMatch = !($scope.passwords.newPassword1==$scope.passwords.newPassword2);
            };
            $scope.responseMessages={
                none:{style:'d-none',description:''},
                error: {style:'alert-danger',description:'Error! Password can not be changed!'},
                success:{style:'alert-success',description:'Success! Password was changed!'}
            }
            $scope.responseMessageDisplayed = $scope.responseMessages.none;
            $scope.responseMessageClose = function(){$scope.responseMessageDisplayed = $scope.responseMessages.none;};

            $scope.changePassword = function(form){
                if(form.$valid && !$scope.newPwdsNotMatch){  
                    var req = {
                        method: 'POST',
                        url: '/AccountSettings/ChangePassword',
                        headers: {
                            'Content-Type': 'application/x-www-form-urlencoded; charset=utf-8'
                        },
                        data: $("#"+form.$name).find(':input').serialize()
                    }               
                    $http(req).then(
                        function(){
                            $scope.responseMessageDisplayed = $scope.responseMessages.success;
                            $scope.passwords={};
                            form.$setPristine();
                            form.$setUntouched();

                        }, 
                        function(){
                            $scope.responseMessageDisplayed = $scope.responseMessages.error;
                        }
                    );
                }
            };

            //Set and run display animation
            var animateElements = function(elementId){
                setTimeout(function(){
                    $('#'+ elementId).find('div').each(function() {
                        var el = $(this);
                        //Animate child controls to appear in sequence
                        $(document).queue(function(next) {
                            el.animate(
                                {opacity: 1},
                                {
                                    queue: true, 
                                    duration: 100, 
                                    specialEasing: {top: 'easeOutBack'}, 
                                    complete: next //call next item in the queue             
                                }
                            ); 
                        });
                    });
                },50);
            };

            //Run animation
            animateElements($scope.menuSelected);
});
