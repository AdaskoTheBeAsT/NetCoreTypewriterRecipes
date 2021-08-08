import {
  appStateAdapter,
  appStateReducer,
  fetchAppState,
} from './app-state.slice';

describe('appState reducer', () => {
  it('should handle initial state', () => {
    const expected = appStateAdapter.getInitialState({
      loadingStatus: 'not loaded',
      error: null,
    });

    expect(appStateReducer(undefined, { type: '' })).toEqual(expected);
  });

  it('should handle fetchAppStates', () => {
    let state = appStateReducer(undefined, fetchAppState.pending(null, null));

    expect(state).toEqual(
      expect.objectContaining({
        loadingStatus: 'loading',
        error: null,
        entities: {},
      })
    );

    state = appStateReducer(
      state,
      fetchAppState.fulfilled([{ id: 1 }], null, null)
    );

    expect(state).toEqual(
      expect.objectContaining({
        loadingStatus: 'loaded',
        error: null,
        entities: { 1: { id: 1 } },
      })
    );

    state = appStateReducer(
      state,
      fetchAppState.rejected(new Error('Uh oh'), null, null)
    );

    expect(state).toEqual(
      expect.objectContaining({
        loadingStatus: 'error',
        error: 'Uh oh',
        entities: { 1: { id: 1 } },
      })
    );
  });
});
