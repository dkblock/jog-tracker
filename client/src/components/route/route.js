import React from 'react';
import { Route as ReactRoute } from 'react-router';

import routes from '../../utils/routes';
import Redirect from './redirect';

const Route = ({ component, path, exact = false }) => {
  const Component = component;
  return <ReactRoute path={path} exact={exact} render={(props) => <Component {...props} />} />;
};

Route.routes = routes;
Route.Redirect = Redirect;
export default Route;
