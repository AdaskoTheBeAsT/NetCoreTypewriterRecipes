module.exports = {
  displayName: 'client-app',
  preset: '../../jest.preset.js',
  setupFilesAfterEnv: ['<rootDir>/src/test-setup.ts'],
  globals: {
    'ts-jest': {
      tsconfig: '<rootDir>/tsconfig.spec.json',
      stringifyContentPathRegex: '\\.(html|svg)$',
      astTransformers: {
        before: [
          'jest-preset-angular/build/InlineFilesTransformer',
          'jest-preset-angular/build/StripStylesTransformer',
        ],
      },
    },
  },
  coverageDirectory:
    '../../../reports/frontendunittest/apps/client-app/coverage',
  snapshotSerializers: [
    'jest-preset-angular/build/serializers/no-ng-attributes',
    'jest-preset-angular/build/serializers/ng-snapshot',
    'jest-preset-angular/build/serializers/html-comment',
  ],
  collectCoverage: true,
  coverageReporters: ['html', 'lcov'],
  reporters: [
    'default',
    [
      'jest-stare',
      {
        resultDir: '../../../reports/frontendunittest/apps/client-app/',
        reportTitle: 'Frontend test',
        additionalResultsProcessors: [],
        coverageLink: 'coverage/index.html',
        resultJson: 'frontend.stare.json',
        resultHtml: 'frontend.stare.html',
        report: true,
        reportSummary: true,
      },
    ],
    [
      'jest-html-reporters',
      {
        publicPath: '../../../reports/frontendunittest/apps/client-app/',
        filename: 'frontend-test-report.html',
        pageTitle: 'Frontend test',
        expand: true,
      },
    ],
    [
      'jest-xunit',
      {
        outputPath: '../../../reports/frontendunittest/apps/client-app/',
        filename: 'frontend-test-report.xunit.xml',
        traitsRegex: [
          { regex: /\(Test Type:([^,)]+)(,|\)).*/g, name: 'Category' },
          { regex: /.*Test Traits: ([^)]+)\).*/g, name: 'Type' },
        ],
      },
    ],
    [
      'jest-sonar',
      {
        outputDirectory: '../../../reports/frontendunittest/apps/client-app/',
        outputName: 'frontend-test.sonar.xml',
      },
    ],
    [
      'jest-trx-results-processor',
      {
        outputFile:
          '../../../reports/frontendunittest/apps/client-app/frontend-test.trx',
      },
    ],
  ],
};
