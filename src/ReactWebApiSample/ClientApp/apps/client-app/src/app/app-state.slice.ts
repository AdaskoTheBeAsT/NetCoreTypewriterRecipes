import {
  EntityState,
  PayloadAction,
  createAsyncThunk,
  createEntityAdapter,
  createSelector,
  createSlice,
} from '@reduxjs/toolkit';

export const APP_STATE_FEATURE_KEY = 'appState';

/*
 * Update these interfaces according to your requirements.
 */
export interface AppStateEntity {
  id: number;
}

export interface AppStateState extends EntityState<AppStateEntity> {
  loadingStatus: 'not loaded' | 'loading' | 'loaded' | 'error';
  error: string | null | undefined;
}

export const appStateAdapter = createEntityAdapter<AppStateEntity>();

/**
 * Export an effect using createAsyncThunk from
 * the Redux Toolkit: https://redux-toolkit.js.org/api/createAsyncThunk
 *
 * e.g.
 * ```
 * import React, { useEffect } from 'react';
 * import { useDispatch } from 'react-redux';
 *
 * // ...
 *
 * const dispatch = useDispatch();
 * useEffect(() => {
 *   dispatch(fetchAppState())
 * }, [dispatch]);
 * ```
 */
export const fetchAppState = createAsyncThunk(
  'appState/fetchStatus',
  async (_, thunkAPI) => {
    /**
     * Replace this with your custom fetch call.
     * For example, `return myApi.getAppStates()`;
     * Right now we just return an empty array.
     */
    return Promise.resolve([]);
  }
);

export const initialAppStateState: AppStateState =
  appStateAdapter.getInitialState({
    loadingStatus: 'not loaded',
    error: null,
  });

export const appStateSlice = createSlice({
  name: APP_STATE_FEATURE_KEY,
  initialState: initialAppStateState,
  reducers: {
    add: appStateAdapter.addOne,
    remove: appStateAdapter.removeOne,
    // ...
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchAppState.pending, (state: AppStateState) => {
        state.loadingStatus = 'loading';
      })
      .addCase(
        fetchAppState.fulfilled,
        (state: AppStateState, action: PayloadAction<AppStateEntity[]>) => {
          appStateAdapter.setAll(state, action.payload);
          state.loadingStatus = 'loaded';
        }
      )
      .addCase(fetchAppState.rejected, (state: AppStateState, action) => {
        state.loadingStatus = 'error';
        state.error = action.error.message;
      });
  },
});

/*
 * Export reducer for store configuration.
 */
export const appStateReducer = appStateSlice.reducer;

/*
 * Export action creators to be dispatched. For use with the `useDispatch` hook.
 *
 * e.g.
 * ```
 * import React, { useEffect } from 'react';
 * import { useDispatch } from 'react-redux';
 *
 * // ...
 *
 * const dispatch = useDispatch();
 * useEffect(() => {
 *   dispatch(appStateActions.add({ id: 1 }))
 * }, [dispatch]);
 * ```
 *
 * See: https://react-redux.js.org/next/api/hooks#usedispatch
 */
export const appStateActions = appStateSlice.actions;

/*
 * Export selectors to query state. For use with the `useSelector` hook.
 *
 * e.g.
 * ```
 * import { useSelector } from 'react-redux';
 *
 * // ...
 *
 * const entities = useSelector(selectAllAppState);
 * ```
 *
 * See: https://react-redux.js.org/next/api/hooks#useselector
 */
const { selectAll, selectEntities } = appStateAdapter.getSelectors();

export const getAppStateState = (
  rootState: Record<string, AppStateState>
): AppStateState => rootState[APP_STATE_FEATURE_KEY];

export const selectAllAppState = createSelector(getAppStateState, selectAll);

export const selectAppStateEntities = createSelector(
  getAppStateState,
  selectEntities
);
