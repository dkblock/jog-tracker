import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { login, openSignPage } from '../../redux/actions';
import { useCurrentUser } from '../../hooks';
import accountValidator from '../../utils/validation/account-validator';
import { SELECTORS } from '../../redux';
import { Button, Paper, Route, TextField } from '../../components';

const Login = () => {
  const dispatch = useDispatch();
  const isFetching = useSelector(SELECTORS.ACCOUNT.getIsSendingCredentials);
  const serverErrors = useSelector(SELECTORS.ACCOUNT.getErrors);
  const { isAuthenticated } = useCurrentUser();

  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [errors, setErrors] = useState({});

  useEffect(() => {
    dispatch(openSignPage());
  }, []);

  useEffect(() => {
    setErrors({ ...errors, ...serverErrors });
  }, [serverErrors]);

  const handleUserNameChange = (value) => setUserName(value);
  const handlePasswordChange = (value) => setPassword(value);

  const handleUserNameFocus = () => setErrors((prev) => ({ ...prev, userName: null }));
  const handlePasswordFocus = () => setErrors((prev) => ({ ...prev, password: null }));

  const handleSubmit = () => {
    const credentials = { userName, password };
    const { isValid, errors: nextErrors } = accountValidator.validateOnLogin(credentials);

    if (isValid) {
      setErrors({});
      dispatch(login({ credentials }));
    } else {
      setErrors(nextErrors);
    }
  };

  if (isAuthenticated) {
    return <Route.Redirect to={Route.routes.jogs.main} />;
  }

  return (
    <form className="account-sign">
      <Paper className="input-form account-sign-form">
        <img className="account-sign-form__logo" src="/public/logo.svg" alt="logo" />
        <TextField
          className="account-sign-form__control"
          label="Username"
          value={userName}
          error={Boolean(errors.userName)}
          helperText={errors.userName}
          onChange={handleUserNameChange}
          onFocus={handleUserNameFocus}
        />
        <TextField
          className="account-sign-form__control"
          label="Password"
          value={password}
          type="password"
          error={Boolean(errors.password)}
          helperText={errors.password}
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
