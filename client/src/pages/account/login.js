import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { login } from '../../actions';
import { useCurrentUser } from '../../hooks';
import accountValidator from '../../utils/validation/account-validator';
import { SELECTORS } from '../../store';
import { Button, Paper, Route, TextField } from '../../components';

const Login = () => {
  const dispatch = useDispatch();
  const isFetching = useSelector(SELECTORS.ACCOUNT.getIsSendingCredentials);
  const { isAuthenticated } = useCurrentUser();

  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [validationErrors, setValidationErrors] = useState({});

  const handleUserNameChange = (value) => setUserName(value);
  const handlePasswordChange = (value) => setPassword(value);

  const handleUserNameFocus = () => setValidationErrors((prev) => ({ ...prev, userName: null }));
  const handlePasswordFocus = () => setValidationErrors((prev) => ({ ...prev, password: null }));

  const handleSubmit = () => {
    const credentials = { userName, password };
    const { isValid, validationErrors: nextValidationErrors } = accountValidator.validateOnLogin(credentials);

    if (isValid) {
      setValidationErrors({});
      dispatch(login({ credentials }));
    } else {
      setValidationErrors(nextValidationErrors);
    }
  };

  if (isAuthenticated) {
    return <Route.Redirect to={Route.routes.jogs.main} />;
  }

  return (
    <form className="account-sign">
      <Paper className="account-sign-form">
        <img className="account-sign-form__logo" src="/public/logo.svg" alt="logo" />
        <TextField
          className="account-sign-form__control"
          label="Username"
          value={userName}
          error={Boolean(validationErrors.userName)}
          helperText={validationErrors.userName}
          onChange={handleUserNameChange}
          onFocus={handleUserNameFocus}
        />
        <TextField
          className="account-sign-form__control"
          label="Password"
          value={password}
          type="password"
          error={Boolean(validationErrors.password)}
          helperText={validationErrors.password}
          onChange={handlePasswordChange}
          onFocus={handlePasswordFocus}
        />
        <div className="account-sign-form__submit">
          <Button color={Button.colors.success} isLoading={isFetching} onClick={handleSubmit}>
            {isFetching ? 'Signing in...' : 'Sign in'}
          </Button>
        </div>
      </Paper>
    </form>
  );
};

export default Login;
