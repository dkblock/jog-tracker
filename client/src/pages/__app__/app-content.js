import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { Switch } from 'react-router-dom';

import { authenticate } from '../../actions';
import { useCurrentUser } from '../../hooks';
import Account from '../account';
import Jogs from '../jogs/jogs';
import { ModalRoot, Route } from '../../components';
import './app.scss';

const AppContent = () => {
  const dispatch = useDispatch();
  const { isFetching } = useCurrentUser();

  useEffect(() => {
    dispatch(authenticate());
  }, []);

  return (
    <div className="app-content">
      {!isFetching && (
        <Switch>
          <Route path="/" exact component={Jogs} />
          <Route path={Route.routes.account.main} component={Account} />
          <Route path={Route.routes.jogs.main} component={Jogs} />
        </Switch>
      )}
      <ModalRoot />
    </div>
  );
};

export default AppContent;
