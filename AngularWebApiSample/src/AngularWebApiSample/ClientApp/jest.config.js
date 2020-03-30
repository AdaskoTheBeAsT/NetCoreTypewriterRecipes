const jestConfig = {
  preset: 'jest-preset-angular',
  setupFilesAfterEnv: ['<rootDir>/jest/setup.ts'],
  coverageReporters: ['lcov', 'text'],
  testMatch: [
      '<rootDir>/**/*(*.)@(spec|test).[tj]s?(x)',
  ],
  //  // moduleNameMapper: {
  // //     'app/(.*)': '<rootDir>/src/app/$1',
  // //     'assets/(.*)': '<rootDir>/src/assets/$1',
  // //     'environments/(.*)': '<rootDir>/src/environments/$1',
  // //   },
  // //   transformIgnorePatterns: ['node_modules/(?!@ngrx)'],
  coveragePathIgnorePatterns: [
      '<rootDir>/node_modules/',
      '<rootDir>/out-tsc/',
      '<rootDir>/**/*(*.)@(spec|test).[tj]s?(x)',
      'src/(setup-jest|jest-global-mocks).ts',
  ],
  coverageDirectory: '../../../reports/tscoverage',
  testResultsProcessor: './resultsProcessor',
  reporters: [
    'default',
    ['jest-html-reporters', {
      publicPath: '../../../reports/frontendunittest/',
      filename: 'frontend-test-report.html',
      pageTitle:'Frontend test',
      expand: true
    }],
    ['jest-xunit', {
      outputPath: '../../../reports/frontendunittest/',
      filename: 'frontend-test-report.xunit.xml',
      traitsRegex: [
        { regex: /\(Test Type:([^,)]+)(,|\)).*/g, name: 'Category' },
        { regex: /.*Test Traits: ([^)]+)\).*/g, name: 'Type' }
      ]
    }],
    // ['jest-sonar-reporter', {
    //   reportPath: '../../../reports/frontendunittest/',
    //   reportFile: 'frontend-test.sonar.xml',
    //   indent: 4
    // }],
    // ['jest-nunit-reporter', {
    //   outputPath: '../../../reports/frontendunittest/',
    //   outputFilename: 'frontend-test.nunit.xml'
    // }],
  ]
};

module.exports = jestConfig;
