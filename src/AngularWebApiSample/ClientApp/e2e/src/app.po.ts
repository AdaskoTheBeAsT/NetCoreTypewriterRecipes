import { Selector } from 'testcafe';

import { browser } from '../utils';

export class AppPage {
  navigateTo(): TestControllerPromise {
    return browser.goTo('/');
  }

  getParagraphText(): Promise<string> {
    return Selector('.content span').textContent;
  }
}
