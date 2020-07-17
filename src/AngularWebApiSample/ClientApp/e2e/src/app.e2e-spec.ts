import { AppPage } from './app.po';

let page: AppPage;

fixture('App').beforeEach(async (t) => {
  page = new AppPage();
});

test('should display welcome message', async (t) => {
  await page.navigateTo();

  const paragraphText = await page.getParagraphText();

  await t.expect(paragraphText).contains('ClientApp app is running!');
});
