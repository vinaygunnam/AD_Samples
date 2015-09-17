/// <reference path="typings/requirejs/require.d.ts" />
//require.config({
//    baseUrl: 'Scripts',
//    paths: {
//        jquery: './jquery-2.1.4',
//        'jquery-ui': './jquery-ui.1.11.4',
//        angular: './angular'
//    },
//    shims: {}
//});

//require(["angular"], () => {
//    require(["app/app"], (app) => {
//        app();
//    });
//});

'use strict';

if (window.__karma__) {
    var allTestFiles = [];
    var TEST_REGEXP = /spec\.js$/;

    var pathToModule = function (path) {
        return path.replace(/^\/base\//, '').replace(/\.js$/, '');
    };

    Object.keys(window.__karma__.files).forEach(function (file) {
        if (TEST_REGEXP.test(file)) {
            // Normalize paths to RequireJS module names.
            allTestFiles.push(pathToModule(file));
        }
    });

    console.log(allTestFiles);
}

require.config({
    paths: {
        angular: 'Scripts/angular',
        angularMocks: 'Scripts/angular-mocks',
        app: 'Scripts/app/app'
    },
    shim: {
        'angular': { 'exports': 'angular' },
        'angularRoute': ['angular'],
        'angularMocks': {
            deps: ['angular'],
            'exports': 'angular.mock'
        },
        'app': {
            deps: ['angular']
        }
    },
    priority: [
        "angular"
    ],
    deps: window.__karma__ ? allTestFiles : [],
    callback: window.__karma__ ? window.__karma__.start : null,
    baseUrl: window.__karma__ ? '/base' : '',
});

require(['app'], () => {
    var $html = angular.element(document.getElementsByTagName('html')[0]);
    angular.element().ready(function () {
        // bootstrap the app manually
        angular.bootstrap(document, ['app']);
    });
});