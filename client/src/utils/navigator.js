import routes from './routes';
import { history } from '../redux';

export const navigateToLogin = () => history.push(routes.account.login);
export const navigateToRegister = () => history.push(routes.account.register);

export const navigateToJogs = () => history.push(routes.jogs.main);
export const navigateToUsers = () => history.push(routes.users.main);
