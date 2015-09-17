define(['app', 'angularMocks'], () => {
    describe('log-hello component', () => {
        beforeEach(angular.mock.module('app'));

        let $compile: ng.ICompileService,
            $rootScope: ng.IRootScopeService,
            element: ng.IAugmentedJQuery;

        beforeEach(() => {
            inject((_$compile_: ng.ICompileService, _$rootScope_: ng.IRootScopeService) => {
                $compile = _$compile_;
                $rootScope = _$rootScope_;

                element = $compile('<log-hello>Test</log-hello>')($rootScope);
            });
        });

        it('should log a message to the console', () => {
            expect(element.text()).toBe('Test1');
        });
    });
});