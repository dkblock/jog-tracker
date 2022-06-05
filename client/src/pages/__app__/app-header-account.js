import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import Popover from '@mui/material/Popover';
import MenuItem from '@mui/material/MenuItem';
import ListItemIcon from '@mui/material/ListItemIcon';

import { logout } from '../../store/actions';
import { useCurrentUser } from '../../hooks';
import { navigateToLogin, navigateToRegister } from '../../utils/navigator';
import { Avatar, Button, Icon, Loader } from '../../components';

const AppHeaderAccount = () => {
  const dispatch = useDispatch();
  const { currentUser, isAuthenticated, isFetching } = useCurrentUser();
  const [anchorEl, setAnchorEl] = useState(null);

  const handleMenuOpen = (event) => setAnchorEl(event.currentTarget);
  const handleMenuClose = () => setAnchorEl(null);

  const handleLogoutClick = () => {
    dispatch(logout());
    handleMenuClose();
  };

  return (
    <section className="app-header__section">
      {!isAuthenticated ? (
        <>
          <Button
            className="app-header__button"
            color={Button.colors.transparentBlack}
            onClick={() => navigateToLogin()}
          >
            Sign in
          </Button>
          <Button
            className="app-header__button"
            color={Button.colors.transparentBlack}
            onClick={() => navigateToRegister()}
          >
            Sign up
          </Button>
        </>
      ) : isFetching ? (
        <Loader className="app-header__avatar" size={Loader.sizes.small} />
      ) : (
        <>
          <Avatar
            className="app-header__avatar"
            firstName={currentUser.firstName}
            lastName={currentUser.lastName}
            onClick={handleMenuOpen}
          />
          <Popover
            id="app-header-popover"
            open={Boolean(anchorEl)}
            anchorEl={anchorEl}
            onClose={handleMenuClose}
            anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
            transformOrigin={{ vertical: 'top', horizontal: 'right' }}
          >
            <div className="app-header__menu">
              <MenuItem onClick={handleMenuClose}>
                {currentUser.firstName} {currentUser.lastName}
              </MenuItem>
              <div className="app-header__menu-divider" />
              <MenuItem onClick={handleLogoutClick}>
                <ListItemIcon>
                  <Icon type={Icon.types.LOGOUT} />
                </ListItemIcon>
                Logout
              </MenuItem>
            </div>
          </Popover>
        </>
      )}
    </section>
  );
};

export default AppHeaderAccount;
