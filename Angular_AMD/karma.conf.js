/* global module */
"use strict";

module.exports = function (config) {
    config.set({

        basePath: './',

        files: [
          { pattern: 'Scripts/angular.js', included: false },
          { pattern: 'Scripts/angular-mocks.js', included: false },
          
          { pattern: 'Scripts/app/app.js', included: false },
          { pattern: 'Scripts/app/components/**/*.js', included: false },
          // needs to be last http://karma-runner.github.io/0.12/plus/requirejs.html
          'Scripts/main.js'
        ],

        autoWatch: true,

        frameworks: ['jasmine', 'requirejs'],

        browsers: ['Chrome'],

        plugins: [
                'karma-chrome-launcher',
                'karma-jasmine',
                'karma-requirejs',
                'karma-junit-reporter'
        ],

        junitReporter: {
            outputFile: 'test_out/unit.xml',
            suite: 'unit'
        }

    });
};
