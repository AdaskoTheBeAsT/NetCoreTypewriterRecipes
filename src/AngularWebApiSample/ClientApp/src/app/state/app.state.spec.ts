import { TestBed, waitForAsync } from '@angular/core/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { AppState } from './app.state';
import { AppAction } from './app.actions';

describe('App actions', () => {
  let store: Store;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [NgxsModule.forRoot([AppState])],
    }).compileComponents();
    store = TestBed.inject(Store);
  }));

  it('should create an action and add an item', () => {
    store.dispatch(new AppAction('item-1'));
    store
      .select((state) => state.app.items)
      .subscribe((items: string[]) => {
        expect(items).toEqual(expect.objectContaining(['item-1']));
      });
  });
});
