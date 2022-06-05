import { createSlice } from '@reduxjs/toolkit';

import { onFulfilledDefault, onPendingDefault, onRejectedDefault } from './defaults';
import { generateReport } from '../actions';

const initialState = {
  report: null,
  hasError: false,

  isFetching: false,
  isSaving: false,
};

const reportsReducer = createSlice({
  name: 'REPORTS',
  initialState: initialState,
  extraReducers: {
    [generateReport.pending]: (state) => {
      onPendingDefault(state);
    },
    [generateReport.fulfilled]: (state, { payload: { report, hasError } }) => {
      onFulfilledDefault(state, hasError);
      state.report = report;
    },
    [generateReport.rejected]: (state) => {
      onRejectedDefault(state);
      state.report = null;
    },
  },
}).reducer;

const SELECTORS = {
  getReport: (state) => ({ report: state.REPORTS.report, hasError: state.REPORTS.hasError }),
  getFetching: (state) => state.REPORTS.isFetching,
};

export { SELECTORS, reportsReducer };
