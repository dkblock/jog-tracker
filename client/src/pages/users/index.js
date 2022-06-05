import React from 'react';

import { useCurrentUser } from '../../hooks';
import UsersList from './users-list';
import { Paper, Route } from '../../components';
import './users.scss';

const Users = () => {
  const { isAdministrator } = useCurrentUser();

  if (!isAdministrator) {
    return <Route.Redirect to={Route.routes.jogs.main} />;
  }

  return (
    <div className="jogs-container">
      <Paper className="jogs-list">
        <UsersList />
      </Paper>
    </div>
  );
};

export default Users;
