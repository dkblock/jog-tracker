import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { register } from '../../actions';
import { useCurrentUser } from '../../hooks';
import accountValidator from '../../utils/validation/account-validator';
import { SELECTORS } from '../../store';
import { Button, Paper, Route, TextField } from '../../components';

const Register = () => {
  const dispatch = useDispatch();
  const isFetching = useSelector(SELECTORS.ACCOUNT.getIsSendingCredentials);
  const { isAuthenticated } = useCurrentUser();

  const [userName, setUserName] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [validationErrors, setValidationErrors] = useState({});

  const handleUserNameChange = (value) => setUserName(value);
  const handleFirstNameChange = (value) => setFirstName(value);
  const handleLastNameChange = (value) => setLastName(value);
  const handlePasswordChange = (value) => setPassword(value);
  const handleConfirmPasswordChange = (value) => setConfirmPassword(value);

  const handleUserNameFocus = () => setValidationErrors((prev) => ({ ...prev, userName: null }));
  const handleFirstNameFocus = () => setValidationErrors((prev) => ({ ...prev, firstName: null }));
  const handleLastNameFocus = () => setValidationErrors((prev) => ({ ...prev, lastName: null }));
  const handlePasswordFocus = () => setValidationErrors((prev) => ({ ...prev, password: null }));
  const handleConfirmPasswordFocus = () => setValidationErrors((prev) => ({ ...prev, confirmPassword: null }));

  const handleSubmit = () => {
    const credentials = { userName, firstName, lastName, password, confirmPassword };
    const { isValid, validationErrors: nextValidationErrors } = accountValidator.validateOnRegister(credentials);

    if (isValid) {
      setValidationErrors({});
      dispatch(register({ credentials }));
    } else {
      setValidationErrors(nextValidationErrors);
    }
  };

  if (isAuthenticated) {
    return <Route.Redirect to={Route.routes.jogs.main} />;
  }

  return (
    <div className="account-sign">
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
          label="First name"
          value={firstName}
          error={Boolean(validationErrors.firstName)}
          helperText={validationErrors.firstName}
          onChange={handleFirstNameChange}
          onFocus={handleFirstNameFocus}
        />
        <TextField
          className="account-sign-form__control"
          label="Last name"
          value={lastName}
          error={Boolean(validationErrors.lastName)}
          helperText={validationErrors.lastName}
          onChange={handleLastNameChange}
          onFocus={handleLastNameFocus}
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
        <TextField
          className="account-sign-form__control"
          label="Confirm password"
          value={confirmPassword}
          type="password"
          error={Boolean(validationErrors.confirmPassword)}
          helperText={validationErrors.confirmPassword}
          onChange={handleConfirmPasswordChange}
          onFocus={handleConfirmPasswordFocus}
        />
        <div className="account-sign-form__submit">
          <Button color={Button.colors.success} isLoading={isFetching} onClick={handleSubmit}>
            {isFetching ? 'Signing up...' : 'Sign up'}
          </Button>
        </div>
      </Paper>
    </div>
  );
};

export default Register;
