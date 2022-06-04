import React from 'react';

import { navigateToJogs } from '../../utils/navigator';
import AppHeaderAccount from './app-header-account';

const AppHeader = () => {
  return (
    <header className="app-header">
      <section className="app-header__section">
        <img className="app-header__logo" src="/public/logo.svg" alt="logo" onClick={() => navigateToJogs()} />
      </section>
      <AppHeaderAccount />
    </header>
  );
};

export default AppHeader;
