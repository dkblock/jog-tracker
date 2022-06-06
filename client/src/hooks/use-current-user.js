import { useSelector } from 'react-redux';

import roles from '../constants/roles';
import { SELECTORS } from '../redux';

const useCurrentUser = () => {
  const currentUser = useSelector(SELECTORS.ACCOUNT.getCurrentUser);
  const isAuthenticated = useSelector(SELECTORS.ACCOUNT.getIsAuthenticated);
  const isFetching = useSelector(SELECTORS.ACCOUNT.getIsFetching);
  const isAdministrator = currentUser.role === roles.ADMINISTRATOR;

  return { currentUser, isAuthenticated, isFetching, isAdministrator };
};

export default useCurrentUser;
