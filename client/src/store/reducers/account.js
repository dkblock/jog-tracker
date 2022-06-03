import { createSlice } from '@reduxjs/toolkit';

import { onFulfilledDefault, onPendingDefault, onRejectedDefault } from './defaults';
import { login, register } from '../../actions/account';
import { getAccessToken } from '../../utils/local-storage-manager';

const initialState = {
  currentUser: {},
  validationErrors: {},
  isAuthenticated: !!getAccessToken(),
  isFetching: false,
  hasError: false,
};

const accountReducer = createSlice({
  name: 'ACCOUNT',
  initialState: initialState,
  extraReducers: {},
}).reducer;

const onPending = (state) => {
  onPendingDefault(state);
  state.validationErrors = {};
};

const onFulfilled = (state, { currentUser, isAuthenticated, hasError, validationErrors }) => {
  onFulfilledDefault(state, hasError);
  state.currentUser = currentUser;
  state.isAuthenticated = isAuthenticated;
  state.validationErrors = validationErrors;
};

const onRejected = (state) => {
  onRejectedDefault(state);
  state.currentUser = {};
  state.isAuthenticated = false;
  state.validationErrors = {};
};

const SELECTORS = {
  getJogs: ({ JOGS: state }) => state.jogs,
  getTotalCount: ({ JOGS: state }) => state.totalCount,
  getFilter: ({ JOGS: state }) => ({
    searchText: state.searchText,
    pageIndex: state.pageIndex,
    sortBy: state.sortBy,
    sortDirection: state.sortDirection,
  }),
  isFetching: ({ JOGS: state }) => state.isFetching,
};

export { SELECTORS, accountReducer };
