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
};

module.exports = jestConfig;
