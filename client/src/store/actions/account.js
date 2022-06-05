import { createAsyncThunk } from '@reduxjs/toolkit';

import { accountService } from '../../api';
import statusCode from '../../utils/status-code-reader';
import { navigateToJogs } from '../../utils/navigator';
import {
  clearAccessToken,
  clearRefreshToken,
  setAccessToken,
  setRefreshToken,
} from '../../utils/local-storage-manager';

export const authenticate = createAsyncThunk('authenticate', async () => {
  const response = await accountService.authenticate();

  if (!response) {
    return { currentUser: {}, isAuthenticated: false, hasError: true, validationErrors: {} };
  }

  const currentUser = await response.json();
  return { currentUser, isAuthenticated: true, hasError: false, validationErrors: {} };
});

export const register = createAsyncThunk('register', async ({ credentials }) => {
  const response = await accountService.register(credentials);
  return await getAuthenticationResult(response);
});

export const login = createAsyncThunk('login', async ({ credentials }) => {
  const response = await accountService.login(credentials);
  return await getAuthenticationResult(response);
});

export const logout = createAsyncThunk('logout', async () => {
  await accountService.logout();

  clearAccessToken();
  clearRefreshToken();
  navigateToJogs();
});

const getAuthenticationResult = async (response) => {
  if (statusCode.ok(response)) {
    const { jwt, currentUser } = await response.json();

    setAccessToken(jwt.accessToken);
    setRefreshToken(jwt.refreshToken);
    navigateToJogs();

    return { currentUser, isAuthenticated: true, hasError: false, validationErrors: {} };
  }

  if (statusCode.badRequest(response)) {
    clearAccessToken();
    clearRefreshToken();

    const { errors } = await response.json();
    const validationErrors = errors.reduce((acc, cur) => ({ ...acc, [cur.Field]: cur.Message }), {});

    return { currentUser: {}, isAuthenticated: false, hasError: true, validationErrors };
  }
};
