import { nxE2EPreset } from '@nx/cypress/plugins/cypress-preset';
import { defineConfig } from 'cypress';

export default defineConfig({
  e2e: nxE2EPreset(__dirname),
  fileServerFolder: '.',
  fixturesFolder: './src/fixtures',
  modifyObstructiveCode: false,
  video: true,
  videosFolder: '../../dist/cypress/apps/client-app-e2e/videos',
  screenshotsFolder: '../../dist/cypress/apps/client-app-e2e/screenshots',
  chromeWebSecurity: false,
});
