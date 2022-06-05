import { connectRouter } from 'connected-react-router';

import { accountReducer } from './reducers/account';
import { jogsReducer } from './reducers/jogs';
import { reportsReducer } from './reducers/reports';
import { usersReducer } from './reducers/users';
import { modalReducer } from './reducers/modal';

const createRootReducer = (history) => ({
  router: connectRouter(history),

  ACCOUNT: accountReducer,
  JOGS: jogsReducer,
  REPORTS: reportsReducer,
  USERS: usersReducer,
  MODAL: modalReducer,
});

export default createRootReducer;
