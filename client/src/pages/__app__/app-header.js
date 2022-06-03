import React from 'react';

import { Button } from '../../components/button';
import { navigateToJogs, navigateToLogin, navigateToRegister } from '../../utils/navigator';

const AppHeader = () => {
  return (
    <header className="app-header">
      <section className="app-header__section">
        <img className="app-header__logo" src="/public/logo.svg" alt="logo" onClick={() => navigateToJogs()} />
      </section>
      <section className="app-header__section">
        <Button className="app-header__button" color={Button.colors.transparentBlack} onClick={() => navigateToLogin()}>
          Sign in
        </Button>
        <Button
          className="app-header__button"
          color={Button.colors.transparentBlack}
          onClick={() => navigateToRegister()}
        >
          Sign up
        </Button>
      </section>
    </header>
  );
};

export default AppHeader;
