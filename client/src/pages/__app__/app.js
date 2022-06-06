import React from 'react';
import { ConnectedRouter } from 'connected-react-router';
import { Provider } from 'react-redux';

import { store, history } from '../../redux';
import AppContent from './app-content';
import AppHeader from './app-header';

const App = () => {
  return (
    <Provider store={store}>
      <ConnectedRouter history={history}>
        <AppHeader />
        <AppContent />
      </ConnectedRouter>
    </Provider>
  );
};

export default App;
