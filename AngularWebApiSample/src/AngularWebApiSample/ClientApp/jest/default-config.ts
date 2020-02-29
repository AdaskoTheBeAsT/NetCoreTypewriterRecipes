import {globals} from 'jest-preset-angular/jest-preset.js';

export default {
  globals,
  preset: 'jest-preset-angular',
  testURL: 'https://github.com/@angular-cli-builders',
  setupFilesAfterEnv: ['./setup.ts'],
  moduleNameMapper: {
    '\\.(jpg|jpeg|png)$': './mock-module.ts',
  },
};
