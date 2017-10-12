import { AngularsamplePage } from './app.po';

describe('angularsample App', function() {
  let page: AngularsamplePage;

  beforeEach(() => {
    page = new AngularsamplePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
