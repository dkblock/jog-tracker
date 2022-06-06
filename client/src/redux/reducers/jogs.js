import { createSlice } from '@reduxjs/toolkit';

import { onFulfilledDefault, onPendingDefault, onRejectedDefault } from './defaults';
import { createJog, deleteJog, fetchJogs, updateJog } from '../actions';

const initialState = {
  jogs: [],
  totalCount: 0,
  validationErrors: {},

  searchText: '',
  dateFrom: null,
  dateTo: null,
  showOnlyOwn: false,
  pageIndex: 1,
  pageSize: 20,
  sortBy: 'date',
  sortDirection: 'desc',

  isFetching: false,
  isSaving: false,
};

const jogsReducer = createSlice({
  name: 'JOGS',
  initialState: initialState,
  extraReducers: {
    [fetchJogs.pending]: (state, { meta: { arg } }) => {
      onPendingDefault(state);

      const { searchText, dateFrom, dateTo, pageIndex, sortBy, sortDirection, showOnlyOwn } = arg;
      state.searchText = searchText;
      state.showOnlyOwn = showOnlyOwn;
      state.dateFrom = dateFrom;
      state.dateTo = dateTo;
      state.pageIndex = pageIndex;
      state.sortBy = sortBy;
      state.sortDirection = sortDirection;
    },
    [fetchJogs.fulfilled]: (state, { payload: { jogs, totalCount, hasError } }) => {
      onFulfilledDefault(state, hasError);

      state.jogs = jogs;
      state.totalCount = totalCount;
    },
    [fetchJogs.rejected]: (state) => {
      onRejectedDefault(state);

      state.jogs = [];
      state.totalCount = 0;
    },

    [createJog.pending]: (state) => {
      onPendingDefault(state, 'isSaving');
      state.validationErrors = {};
    },
    [createJog.fulfilled]: (state, { payload: { createdJog, hasError, validationErrors } }) => {
      onFulfilledDefault(state, hasError, 'isSaving');

      if (!hasError) {
        state.jogs = [createdJog, ...state.jogs];
        state.totalCount++;
      } else {
        state.validationErrors = validationErrors;
      }
    },
    [createJog.rejected]: (state) => {
      onRejectedDefault(state, 'isSaving');
    },

    [deleteJog.pending]: (state) => {
      onPendingDefault(state, 'isSaving');
    },
    [deleteJog.fulfilled]: (state, { payload: { jogId, hasError } }) => {
      onFulfilledDefault(state, hasError, 'isSaving');
      if (hasError) return;

      state.jogs = state.jogs.filter((jog) => jog.id !== jogId);
      state.totalCount--;
    },
    [deleteJog.rejected]: (state) => {
      onRejectedDefault(state, 'isSaving');
    },

    [updateJog.pending]: (state) => {
      onPendingDefault(state, 'isSaving');
      state.validationErrors = {};
    },
    [updateJog.fulfilled]: (state, { payload: { updatedJog, hasError, validationErrors } }) => {
      onFulfilledDefault(state, hasError, 'isSaving');

      if (!hasError) {
        state.jogs = state.jogs.map((jog) => (jog.id === updatedJog.id ? updatedJog : jog));
      } else {
        state.validationErrors = validationErrors;
      }
    },
    [updateJog.rejected]: (state) => {
      onRejectedDefault(state, 'isSaving');
    },
  },
}).reducer;

const SELECTORS = {
  getJogs: (state) => state.JOGS.jogs,
  getTotalCount: (state) => state.JOGS.totalCount,
  getFilter: (state) => ({
    searchText: state.JOGS.searchText,
    showOnlyOwn: state.JOGS.showOnlyOwn,
    dateFrom: state.JOGS.dateFrom,
    dateTo: state.JOGS.dateTo,
    pageIndex: state.JOGS.pageIndex,
    pageSize: state.JOGS.pageSize,
    sortBy: state.JOGS.sortBy,
    sortDirection: state.JOGS.sortDirection,
  }),
  getFetching: (state) => ({
    isFetching: state.JOGS.isFetching,
    isSaving: state.JOGS.isSaving,
  }),
  getErrors: (state) => state.JOGS.validationErrors,
};

export { SELECTORS, jogsReducer };
