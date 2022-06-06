import { createAsyncThunk } from '@reduxjs/toolkit';

import { jogsService } from '../../api';
import statusCode from '../../utils/status-code-reader';
import { hideModal } from './modal';

export const fetchJogs = createAsyncThunk('fetchJogs', async (params) => {
  const response = await jogsService.fetchJogs(params);

  if (statusCode.ok(response)) {
    const { page: jogs, totalCount } = await response.json();
    return { jogs, totalCount, hasError: false };
  }

  return { jogs: [], totalCount: 0, hasError: true };
});

export const createJog = createAsyncThunk('createJog', async ({ jog }, thunkAPI) => {
  const response = await jogsService.createJog(jog);

  if (statusCode.created(response)) {
    thunkAPI.dispatch(hideModal());

    const createdJog = await response.json();
    return { createdJog, hasError: false };
  }

  if (statusCode.badRequest(response)) {
    const { errors } = await response.json();
    const validationErrors = errors.reduce((acc, cur) => ({ ...acc, [cur.Field]: cur.Message }), {});

    return { validationErrors, hasError: true };
  }

  return { validationErrors: {}, hasError: true };
});

export const deleteJog = createAsyncThunk('deleteJog', async ({ jogId }, thunkAPI) => {
  const response = await jogsService.deleteJog(jogId);

  if (statusCode.noContent(response)) {
    thunkAPI.dispatch(hideModal());
    return { jogId, hasError: false };
  }

  return { jogId: null, hasError: true };
});

export const updateJog = createAsyncThunk('updateJog', async ({ jog }, thunkAPI) => {
  const response = await jogsService.updateJog(jog);

  if (statusCode.ok(response)) {
    thunkAPI.dispatch(hideModal());

    const updatedJog = await response.json();
    return { updatedJog, hasError: false };
  }

  if (statusCode.badRequest(response)) {
    const { errors } = await response.json();
    const validationErrors = errors.reduce((acc, cur) => ({ ...acc, [cur.Field]: cur.Message }), {});

    return { validationErrors, hasError: true };
  }

  return { validationErrors: {}, hasError: true };
});
