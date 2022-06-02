import { connectRouter } from 'connected-react-router';

import { jogsReducer } from './reducers/jogs';

const createRootReducer = (history) => ({
  router: connectRouter(history),
  JOGS: jogsReducer,
});

export default createRootReducer;
