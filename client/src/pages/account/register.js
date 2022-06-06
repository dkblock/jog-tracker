import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { openSignPage, register } from '../../redux/actions';
import { useCurrentUser } from '../../hooks';
import accountValidator from '../../utils/validation/account-validator';
import { SELECTORS } from '../../redux';
import { Button, Paper, Route, TextField } from '../../components';

const Register = () => {
  const dispatch = useDispatch();
  const isFetching = useSelector(SELECTORS.ACCOUNT.getIsSendingCredentials);
  const serverErrors = useSelector(SELECTORS.ACCOUNT.getErrors);
  const { isAuthenticated } = useCurrentUser();

  const [userName, setUserName] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [errors, setErrors] = useState({});

  useEffect(() => {
    dispatch(openSignPage());
  }, []);

  useEffect(() => {
    setErrors({ ...errors, ...serverErrors });
  }, [serverErrors]);

  const handleUserNameChange = (value) => setUserName(value);
  const handleFirstNameChange = (value) => setFirstName(value);
  const handleLastNameChange = (value) => setLastName(value);
  const handlePasswordChange = (value) => setPassword(value);
  const handleConfirmPasswordChange = (value) => setConfirmPassword(value);

  const handleUserNameFocus = () => setErrors((prev) => ({ ...prev, userName: null }));
  const handleFirstNameFocus = () => setErrors((prev) => ({ ...prev, firstName: null }));
  const handleLastNameFocus = () => setErrors((prev) => ({ ...prev, lastName: null }));
  const handlePasswordFocus = () => setErrors((prev) => ({ ...prev, password: null }));
  const handleConfirmPasswordFocus = () => setErrors((prev) => ({ ...prev, confirmPassword: null }));

  const handleSubmit = () => {
    const credentials = { userName, firstName, lastName, password, confirmPassword };
    const { isValid, errors: nextErrors } = accountValidator.validateOnRegister(credentials);

    if (isValid) {
      setErrors({});
      dispatch(register({ credentials }));
    } else {
      setErrors(nextErrors);
    }
  };

  if (isAuthenticated) {
    return <Route.Redirect to={Route.routes.jogs.main} />;
  }

  return (
    <div className="account-sign">
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
          label="First name"
          value={firstName}
          error={Boolean(errors.firstName)}
          helperText={errors.firstName}
          onChange={handleFirstNameChange}
          onFocus={handleFirstNameFocus}
        />
        <TextField
          className="account-sign-form__control"
          label="Last name"
          value={lastName}
          error={Boolean(errors.lastName)}
          helperText={errors.lastName}
          onChange={handleLastNameChange}
          onFocus={handleLastNameFocus}
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
        <TextField
          className="account-sign-form__control"
          label="Confirm password"
          value={confirmPassword}
          type="password"
          error={Boolean(errors.confirmPassword)}
          helperText={errors.confirmPassword}
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
