import { createSlice } from '@reduxjs/toolkit';

import { onFulfilledDefault, onPendingDefault, onRejectedDefault } from './defaults';
import { fetchJogs } from '../../actions/jogs';

const initialState = {
  jogs: [],
  totalCount: 0,

  searchText: '',
  pageIndex: 1,
  pageSize: 20,
  sortBy: 'date',
  sortDirection: 'desc',

  isFetching: false,
};

const jogsReducer = createSlice({
  name: 'JOGS',
  initialState: initialState,
  extraReducers: {
    [fetchJogs.pending]: (state, { meta: { arg } }) => {
      onPendingDefault(state);

      const { searchText, pageIndex, sortBy, sortDirection } = arg;
      state.searchText = searchText;
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
  },
}).reducer;

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

export { SELECTORS, jogsReducer };
