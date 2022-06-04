import { useSelector } from 'react-redux';
import { SELECTORS } from '../store';

const useCurrentUser = () => {
  const currentUser = useSelector(SELECTORS.ACCOUNT.getCurrentUser);
  const isAuthenticated = useSelector(SELECTORS.ACCOUNT.getIsAuthenticated);
  const isFetching = useSelector(SELECTORS.ACCOUNT.getIsFetching);

  return { currentUser, isAuthenticated, isFetching };
};

export default useCurrentUser;
