import { SELECTORS as ACCOUNT_SELECTORS } from './reducers/account';
import { SELECTORS as JOGS_SELECTORS } from './reducers/jogs';
import { SELECTORS as REPORTS_SELECTORS } from './reducers/reports';
import { SELECTORS as USERS_SELECTORS } from './reducers/users';
import { SELECTORS as MODAL_SELECTORS } from './reducers/modal';

const SELECTORS = {
  ACCOUNT: ACCOUNT_SELECTORS,
  JOGS: JOGS_SELECTORS,
  REPORTS: REPORTS_SELECTORS,
  USERS: USERS_SELECTORS,
  MODAL: MODAL_SELECTORS,
};

export default SELECTORS;
