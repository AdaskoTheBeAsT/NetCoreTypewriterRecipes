import { configureStore } from '@reduxjs/toolkit';
import { StrictMode } from 'react';
import * as ReactDOM from 'react-dom/client';
import { Provider } from 'react-redux';

import App from './app/app';
import { APP_STATE_FEATURE_KEY, appStateReducer } from './app/app-state.slice';

const store = configureStore({
  reducer: { [APP_STATE_FEATURE_KEY]: appStateReducer },
  devTools: process.env.NODE_ENV !== 'production',
});

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <Provider store={store}>
    <StrictMode>
      <App />
    </StrictMode>
  </Provider>
);
