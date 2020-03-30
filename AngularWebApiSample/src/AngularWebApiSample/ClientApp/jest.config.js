const jestConfig = {
  preset: 'jest-preset-angular',
  setupFilesAfterEnv: ['<rootDir>/jest/setup.ts', 'jest-allure/dist/setup'],
  coverageReporters: ['lcov', 'text'],
  testMatch: [
      '<rootDir>/**/*(*.)@(spec|test).[tj]s?(x)',
  ],
  // moduleNameMapper: {
  //     'app/(.*)': '<rootDir>/src/app/$1',
  //     'assets/(.*)': '<rootDir>/src/assets/$1',
  //     'environments/(.*)': '<rootDir>/src/environments/$1',
  //   },
  //   transformIgnorePatterns: ['node_modules/(?!@ngrx)'],
  coveragePathIgnorePatterns: [
      '<rootDir>/node_modules/',
      '<rootDir>/out-tsc/',
      '<rootDir>/jest/',
      '.*\.html',
      '.*\.spec\.ts',
      '<rootDir>/src/(setup-jest|jest-global-mocks|global-mocks).ts',
  ],
  coverageDirectory: '../../../reports/tscoverage',
  testResultsProcessor: './resultsProcessor',
  reporters: [
    'default',
    [
      'jest-stare',
      {
        resultDir: '../../../reports/frontendunittest/',
        reportTitle: 'Frontend test',
        additionalResultsProcessors: [
        ],
        coverageLink: '../tscoverage/lcov-report/index.html',
        resultJson: 'frontend.stare.json',
        resultHtml: 'frontend.stare.html',
        report: true,
        reportSummary: true,
      }
    ],
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
  ]
};

module.exports = jestConfig;
