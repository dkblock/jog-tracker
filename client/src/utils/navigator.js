import routes from './routes';
import { history } from '../store';

export const navigateToJogs = () => history.push(routes.jogs());
