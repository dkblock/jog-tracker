import React from 'react';
import { Route as ReactRoute } from 'react-router';

const Route = ({ component, path, exact = false }) => {
  const Component = component;
  return <ReactRoute path={path} exact={exact} render={(props) => <Component {...props} />} />;
};

export default Route;
