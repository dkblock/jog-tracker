import React from 'react';

import routes from '../../utils/routes';
import { Route } from '../../components/route';
import Login from './login';
import Register from './register';
import './account.scss';

const Account = () => {
  return (
    <div className="account">
      <Route path={routes.account.login} exact={false} component={Login} />
      <Route path={routes.account.register} exact={true} component={Register} />
    </div>
  );
};

export default Account;
