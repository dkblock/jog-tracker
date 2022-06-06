import { createAsyncThunk } from '@reduxjs/toolkit';

import { usersService } from '../../api';
import statusCode from '../../utils/status-code-reader';
import { hideModal } from './modal';

export const fetchUsers = createAsyncThunk('fetchUsers', async (params) => {
  const response = await usersService.fetchUsers(params);

  if (statusCode.ok(response)) {
    const { page: users, totalCount } = await response.json();
    return { users, totalCount, hasError: false };
  }

  return { users: [], totalCount: 0, hasError: true };
});

export const deleteUser = createAsyncThunk('deleteUser', async ({ userId }, thunkAPI) => {
  const response = await usersService.deleteUser(userId);

  if (statusCode.noContent(response)) {
    thunkAPI.dispatch(hideModal());
    return { userId, hasError: false };
  }

  return { userId: null, hasError: true };
});

export const updateUser = createAsyncThunk('updateUser', async ({ user }, thunkAPI) => {
  const response = await usersService.updateUser(user);

  if (statusCode.ok(response)) {
    thunkAPI.dispatch(hideModal());

    const updatedUser = await response.json();
    return { updatedUser, hasError: false };
  }

  if (statusCode.badRequest(response)) {
    const { errors } = await response.json();
    const validationErrors = errors.reduce((acc, cur) => ({ ...acc, [cur.Field]: cur.Message }), {});

    return { validationErrors, hasError: true };
  }

  return { validationErrors: {}, hasError: true };
});
