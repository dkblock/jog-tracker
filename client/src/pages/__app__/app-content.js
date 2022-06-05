import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { Switch } from 'react-router-dom';

import { authenticate } from '../../store/actions';
import { useCurrentUser } from '../../hooks';
import Account from '../account';
import Jogs from '../jogs';
import Users from '../users';
import ModalRoot from './modal-root';
import { Route } from '../../components';
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
          <Route path={Route.routes.users.main} component={Users} />
        </Switch>
      )}
      <ModalRoot />
    </div>
  );
};

export default AppContent;
