/// <reference path="../../typings/angularjs/angular.d.ts" />
function logHello() {
    var directive: ng.IDirective = {
        restrict: 'EA',
        template: `<h1 ng-transclude></h1>`,
        transclude: true,
        link() {
            console.log('Hello ', new Date());
        }
    };

    return directive;
}

export = logHello;