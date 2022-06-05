import { SELECTORS as ACCOUNT_SELECTORS } from './reducers/account';
import { SELECTORS as JOGS_SELECTORS } from './reducers/jogs';
import { SELECTORS as MODAL_SELECTORS } from './reducers/modal';

const SELECTORS = {
  ACCOUNT: ACCOUNT_SELECTORS,
  JOGS: JOGS_SELECTORS,
  MODAL: MODAL_SELECTORS,
};

export default SELECTORS;
