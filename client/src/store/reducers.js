import { connectRouter } from 'connected-react-router';

import { accountReducer } from './reducers/account';
import { jogsReducer } from './reducers/jogs';
import { usersReducer } from './reducers/users';
import { modalReducer } from './reducers/modal';

const createRootReducer = (history) => ({
  router: connectRouter(history),

  ACCOUNT: accountReducer,
  JOGS: jogsReducer,
  USERS: usersReducer,
  MODAL: modalReducer,
});

export default createRootReducer;
