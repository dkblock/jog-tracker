import React from 'react';

import Login from './login';
import Register from './register';
import { Route } from '../../components/route';
import './account.scss';

const Account = () => {
  return (
    <div className="account">
      <Route path={Route.routes.account.login} component={Login} />
      <Route path={Route.routes.account.register} component={Register} />
    </div>
  );
};

export default Account;
