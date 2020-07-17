import { Selector } from 'testcafe';

import { browser } from '../utils';

export class AppPage {
  navigateTo() {
    return browser.goTo('/');
  }

  getParagraphText() {
    return Selector('.content span').textContent;
  }
}
