import { createSlice } from '@reduxjs/toolkit';

import { onFulfilledDefault, onPendingDefault, onRejectedDefault } from './defaults';
import { deleteUser, fetchUsers, updateUser } from '../actions';
import roles from '../../constants/roles';

const initialState = {
  users: [],
  totalCount: 0,
  validationErrors: {},

  searchText: '',
  role: roles.ANY,
  pageIndex: 1,
  pageSize: 20,
  sortBy: 'lastName',
  sortDirection: 'asc',

  isFetching: false,
  isSaving: false,
};

const usersReducer = createSlice({
  name: 'USERS',
  initialState: initialState,
  extraReducers: {
    [fetchUsers.pending]: (state, { meta: { arg } }) => {
      onPendingDefault(state);

      const { searchText, role, pageIndex, sortBy, sortDirection } = arg;
      state.searchText = searchText;
      state.role = role;
      state.pageIndex = pageIndex;
      state.sortBy = sortBy;
      state.sortDirection = sortDirection;
    },
    [fetchUsers.fulfilled]: (state, { payload: { users, totalCount, hasError } }) => {
      onFulfilledDefault(state, hasError);

      state.users = users;
      state.totalCount = totalCount;
    },
    [fetchUsers.rejected]: (state) => {
      onRejectedDefault(state);

      state.users = [];
      state.totalCount = 0;
    },

    [deleteUser.pending]: (state) => {
      onPendingDefault(state, 'isSaving');
    },
    [deleteUser.fulfilled]: (state, { payload: { userId, hasError } }) => {
      onFulfilledDefault(state, hasError, 'isSaving');
      if (hasError) return;

      state.users = state.users.filter((user) => user.id !== userId);
      state.totalCount--;
    },
    [deleteUser.rejected]: (state) => {
      onRejectedDefault(state, 'isSaving');
    },

    [updateUser.pending]: (state) => {
      onPendingDefault(state, 'isSaving');
      state.validationErrors = {};
    },
    [updateUser.fulfilled]: (state, { payload: { updatedUser, hasError, validationErrors } }) => {
      onFulfilledDefault(state, hasError, 'isSaving');

      if (!hasError) {
        state.users = state.users.map((user) => (user.id === updatedUser.id ? updatedUser : user));
      } else {
        state.validationErrors = validationErrors;
      }
    },
    [updateUser.rejected]: (state) => {
      onRejectedDefault(state, 'isSaving');
    },
  },
}).reducer;

const SELECTORS = {
  getUsers: (state) => state.USERS.users,
  getTotalCount: (state) => state.USERS.totalCount,
  getFilter: (state) => ({
    searchText: state.USERS.searchText,
    role: state.USERS.role,
    pageIndex: state.USERS.pageIndex,
    pageSize: state.USERS.pageSize,
    sortBy: state.USERS.sortBy,
    sortDirection: state.USERS.sortDirection,
  }),
  getFetching: (state) => ({
    isFetching: state.USERS.isFetching,
    isSaving: state.USERS.isSaving,
  }),
  getErrors: (state) => state.USERS.validationErrors,
};

export { SELECTORS, usersReducer };
