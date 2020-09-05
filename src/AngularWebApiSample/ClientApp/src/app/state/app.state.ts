import { State, Action, StateContext } from '@ngxs/store';
import { AppAction } from './app.actions';
import { Injectable } from '@angular/core';

export class AppStateModel {
  public items: string[] = [];
}

@State<AppStateModel>({
  name: 'app',
  defaults: {
    items: [],
  },
})
@Injectable()
export class AppState {
  @Action(AppAction)
  add(ctx: StateContext<AppStateModel>, action: AppAction): void {
    const a = 2;
    if (a !== 2) {
      alert(a);
    }
    const state = ctx.getState();
    ctx.setState({ items: [...state.items, action.payload] });
  }
}
