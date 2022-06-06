import { createSlice } from '@reduxjs/toolkit';

import { authenticate, login, logout, openSignPage, register } from '../actions';
import { getAccessToken } from '../../utils/local-storage-manager';

const initialState = {
  currentUser: {},
  validationErrors: {},
  isAuthenticated: !!getAccessToken(),
  isFetching: true,
  isSendingCredentials: false,
  hasError: false,
};

const accountReducer = createSlice({
  name: 'ACCOUNT',
  initialState: initialState,
  extraReducers: {
    [openSignPage]: (state) => {
      state.validationErrors = {};
    },

    [authenticate.pending]: (state) => {
      onPending(state, 'isFetching');
    },
    [authenticate.fulfilled]: (state, { payload }) => {
      onFulfilled(state, payload, 'isFetching');
    },
    [authenticate.rejected]: (state) => {
      onRejected(state, 'isFetching');
    },

    [login.pending]: (state) => {
      onPending(state);
    },
    [login.fulfilled]: (state, { payload }) => {
      onFulfilled(state, payload);
    },
    [login.rejected]: (state) => {
      onRejected(state);
    },

    [register.pending]: (state) => {
      onPending(state);
    },
    [register.fulfilled]: (state, { payload }) => {
      onFulfilled(state, payload);
    },
    [register.rejected]: (state) => {
      onRejected(state);
    },

    [logout.pending]: (state) => {
      state.currentUser = {};
      state.isAuthenticated = false;
      state.isFetching = false;
      state.isSendingCredentials = false;
      state.validationErrors = {};
      state.hasError = false;
    },
  },
}).reducer;

const onPending = (state, isFetchingProp = 'isSendingCredentials') => {
  state[isFetchingProp] = true;
  state.hasError = false;
  state.validationErrors = {};
};

const onFulfilled = (
  state,
  { currentUser, isAuthenticated, hasError, validationErrors },
  isFetchingProp = 'isSendingCredentials',
) => {
  state[isFetchingProp] = false;
  state.currentUser = currentUser;
  state.isAuthenticated = isAuthenticated;
  state.validationErrors = validationErrors;
  state.hasError = hasError;
};

const onRejected = (state, isFetchingProp = 'isSendingCredentials') => {
  state[isFetchingProp] = false;
  state.currentUser = {};
  state.isAuthenticated = false;
  state.validationErrors = {};
  state.hasError = true;
};

const SELECTORS = {
  getCurrentUser: (state) => state.ACCOUNT.currentUser,
  getIsAuthenticated: (state) => state.ACCOUNT.isAuthenticated,
  getIsFetching: (state) => state.ACCOUNT.isFetching,
  getIsSendingCredentials: (state) => state.ACCOUNT.isSendingCredentials,
  getErrors: (state) => state.ACCOUNT.validationErrors,
};

export { SELECTORS, accountReducer };
