import React from 'react';
import { Route, Switch } from 'react-router-dom';

import routes from '../../utils/routes';
import Jogs from '../jogs/jogs';
import './app.scss';

const AppContent = () => {
  return (
    <div className="app-content">
      <Switch>
        <Route path="/" component={Jogs} />
        <Route path={routes.jogs()} component={Jogs} />
      </Switch>
    </div>
  );
};

export default AppContent;
