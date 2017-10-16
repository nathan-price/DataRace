// Preload Resources
var resources = {
	"basecode": "static/race/code/",
	"baseimg": "static/race/img/",
	"code": ["prerace.html","prerace.css","race.html","postrace.html"],
	"img": ["ca-hockey.png","eu-napoleon.png","jp-ninja.png","us-liberty.png"]
}
window.onload = function() {
	setTimeout(function() {
		for (let code of resources.code) preload(resources.basecode+code);
		for (let img of resources.img) new Image().src = resources.baseimg+img;
	}, 1000);
};
function preload(uri) {
	let xhr = new XMLHttpRequest();
	xhr.open('GET', uri);
	xhr.send('');
}

// Currency Data
currency.add(
	["CAD", "Canada", resources.baseimg+"ca-hockey.png"],
	["EUR", "Euro", resources.baseimg+"eu-napoleon.png"],
	["GBP", "Britain", resources.baseimg+"gb-guard.png"],
	["JPY", "Japan", resources.baseimg+"jp-ninja.png"],
	["USD", "United States", resources.baseimg+"us-liberty.png"]
);
currency.addPairs('CAD|JPY','EUR|CAD','EUR|GBP','EUR|JPY','EUR|USD','GBP|CAD','GBP|JPY','GBP|USD','USD|CAD','USD|JPY');

// Animation
var pair, onrace;

// Angular App
var app = angular.module('raceApp', ['material.svgAssetsCache', 'ngMaterial', 'ngMessages', 'ngRoute', 'ui.router']);
app.controller('ctrl', function ($scope, $interval, $timeout, $state){
	$state.go('prerace');
	$scope.duration = 20;
	$scope.pair = new currencyPair(null, null);
	$scope.start = function() {
		onrace = true;
		pair = $scope.pair;
		$state.go('race');
		$scope.fireRef = firebase.database().ref('Quotes/'+$scope.pair.ticker);
		$scope.fireRef.on('value', $scope.update);
		setTimeout($scope.end, $scope.duration * 1000);
	}
	$scope.update = function(snapshot) {
		$scope.data = snapshot.val();
		console.log(JSON.stringify($scope.data));
	}
	$scope.end = function() {
		onrace = false;
		$scope.fireRef.off();
		$state.go('postrace');
	}
	$scope.back = function() {
		$state.go('prerace');
	}
});
app.config(function($stateProvider, $urlRouterProvider) {
	$stateProvider.state('prerace', {
		templateUrl: resources.basecode+'prerace.html'
	})
	$stateProvider.state('race', {
		templateUrl: resources.basecode+'race.html'
	})
	$stateProvider.state('postrace', {
		templateUrl: resources.basecode+'postrace.html'
	})
});