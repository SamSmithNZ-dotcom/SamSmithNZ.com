﻿(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .controller('groupController', groupController);
    groupController.$inject = ['$scope', '$http', 'groupCodeService', 'groupService', 'gameService'];

    function groupController($scope, $http, groupCodeService, groupService, gameService) {

        $scope.groupCodes = [];
        $scope.groups = [];
        $scope.games = [];

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetGroupCodesEventComplete = function (response) {
            $scope.groupCodes = response.data;
            //console.log($scope.tabs);
            if ($scope.groupCodes != null && $scope.groupCodes.length > 0 && $scope.roundCode == '') {
                console.log('setting round code from ' + $scope.roundCode + ' to ' + $scope.groupCodes[0].RoundCode);
                $scope.roundCode = $scope.groupCodes[0].RoundCode;
            }
            $scope.updateGroupDetails($scope.tournamentCode, $scope.roundNumber, $scope.roundCode);
        }

        var onGetGroupsEventComplete = function (response) {
            $scope.groups = response.data;
        }

        var onGetGamesEventComplete = function (response) {
            $scope.games = response.data;
            //console.log($scope.games);
        }

        $scope.tournamentCode = getUrlParameter('TournamentCode');
        $scope.roundNumber = getUrlParameter('RoundNumber');
        $scope.roundCode = getUrlParameter('RoundCode');
        $scope.isLastRound = getUrlParameter('IsLastRound');
        //console.log("isLastRound: " + $scope.isLastRound);

        groupCodeService.getGroupCodes($scope.tournamentCode, $scope.roundNumber).then(onGetGroupCodesEventComplete, onError);

        $scope.updateGroupDetails = function (tournamentCode, roundNumber, roundCode) {
            groupService.getGroups(tournamentCode, roundNumber, roundCode).then(onGetGroupsEventComplete, onError);
            gameService.getGamesForGroup(tournamentCode, roundNumber, roundCode).then(onGetGamesEventComplete, onError);
        };

        //Style the group rows depending on the the status of the group
        $scope.getRowStyle = function (hasQualifiedForNextRound, groupRanking, isLastRound) {
            var trStyle = "";
            if (isLastRound == true) {
                switch (groupRanking) {
                    case 1:
                        trStyle = "gold";
                        break;
                    case 2:
                        trStyle = "silver";
                        break;
                    case 3:
                        trStyle = "#A67D3D";
                        break;
                }
            }
            else {
                if (hasQualifiedForNextRound == true) {
                    trStyle = "#CCFF99";
                }
            }
            return trStyle ;
        };

    }

    function getUrlParameter(param: string) {
        var sPageURL: string = (window.location.search.substring(1));
        var sURLVariables: string[] = sPageURL.split(/[&||?]/);
        var res: string;

        for (var i = 0; i < sURLVariables.length; i += 1) {
            var paramName = sURLVariables[i], sParameterName = (paramName || '').split('=');

            //console.log(sParameterName[0].toLowerCase() + ' : ' + param.toLowerCase());
            if (sParameterName[0].toLowerCase() === param.toLowerCase()) {
                res = sParameterName[1];
                //console.log(sParameterName[0] + ' : ' + sParameterName[1]);
            }
        }

        return res;
    }

})();
