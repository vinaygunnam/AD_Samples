/// <reference path="../typings/angularjs/angular.d.ts" />
import logHello = require('Scripts/app/components/log-hello');
export = angular.module("app", [])
    .directive('logHello', logHello);