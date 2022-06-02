import React from 'react';
import { navigateToJogs } from '../../utils/navigator';

const AppHeader = () => {
  return (
    <header className="app-header">
      <section className="app-header__section">
        <img className="app-header__logo" src="/public/logo.svg" alt="logo" onClick={() => navigateToJogs()} />
      </section>
    </header>
  );
};

export default AppHeader;
