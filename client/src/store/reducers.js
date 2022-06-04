import { connectRouter } from 'connected-react-router';

import { accountReducer } from './reducers/account';
import { modalReducer } from './reducers/modal';
import { jogsReducer } from './reducers/jogs';

const createRootReducer = (history) => ({
  router: connectRouter(history),

  ACCOUNT: accountReducer,
  MODAL: modalReducer,
  JOGS: jogsReducer,
});

export default createRootReducer;
