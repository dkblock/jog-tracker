import React from 'react';

import { useCurrentUser } from '../../hooks';
import { navigateToJogs, navigateToUsers } from '../../utils/navigator';
import AppHeaderAccount from './app-header-account';
import { Button } from '../../components';

const AppHeader = () => {
  const { isAdministrator } = useCurrentUser();
  return (
    <header className="app-header">
      <section className="app-header__section">
        <img className="app-header__logo" src="/public/logo.svg" alt="logo" onClick={() => navigateToJogs()} />
        <Button className="app-header__button" color={Button.colors.transparentBlack} onClick={() => navigateToJogs()}>
          JOG RESULTS
        </Button>
        {isAdministrator && (
          <Button
            className="app-header__button"
            color={Button.colors.transparentBlack}
            onClick={() => navigateToUsers()}
          >
            USERS
          </Button>
        )}
      </section>
      <AppHeaderAccount />
    </header>
  );
};

export default AppHeader;
