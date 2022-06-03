import React from 'react';
import { Switch } from 'react-router-dom';

import routes from '../../utils/routes';
import { Route } from '../../components/route';
import Account from '../account';
import Jogs from '../jogs/jogs';
import './app.scss';

const AppContent = () => {
  return (
    <div className="app-content">
      <Switch>
        <Route path="/" exact component={Jogs} />
        <Route path={routes.account.main} component={Account} />
        <Route path={routes.jogs.main} component={Jogs} />
      </Switch>
    </div>
  );
};

export default AppContent;
