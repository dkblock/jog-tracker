import { createAsyncThunk } from '@reduxjs/toolkit';

import accountService from '../api/services/account-service';
import statusCode from '../utils/status-code-reader';
import { navigateToJogs } from '../utils/navigator';
import {
  clearAccessToken,
  clearRefreshToken,
  getAccessToken,
  getRefreshToken,
  setAccessToken,
  setRefreshToken,
} from '../utils/local-storage-manager';

export const authenticate = createAsyncThunk('authenticate', async () => {
  if (!getAccessToken()) {
    return { currentUser: {}, isAuthenticated: false };
  }

  const response = await accountService.auth();
  return await getAuthenticationResult(response);
});

export const register = createAsyncThunk('register', async ({ credentials }) => {
  const response = await accountService.register(credentials);
  return await getAuthenticationResult(response);
});

export const login = createAsyncThunk('login', async ({ credentials }) => {
  const response = await accountService.login(credentials);
  const result = await getAuthenticationResult(response);
  const { hasError } = result;

  if (!hasError) {
    navigateToJogs();
  }

  return result;
});

const getAuthenticationResult = async (response) => {
  if (statusCode.ok(response)) {
    const { jwt, currentUser } = await response.json();

    setAccessToken(jwt.accessToken);
    setRefreshToken(jwt.refreshToken);

    return { currentUser, isAuthenticated: true, hasError: false, validationErrors: {} };
  }

  if (statusCode.badRequest(response)) {
    clearAccessToken();
    clearRefreshToken();

    const { errors } = await response.json();
    const validationErrors = errors.reduce((acc, cur) => ({ ...cur, [acc.field]: acc.message }), {});

    return { currentUser: {}, isAuthenticated: false, hasError: true, validationErrors };
  }
};
