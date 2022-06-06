import { createAction, createAsyncThunk } from '@reduxjs/toolkit';

import { reportsService } from '../../api';
import statusCode from '../../utils/status-code-reader';

export const generateReport = createAsyncThunk('generateReport', async (params) => {
  const response = await reportsService.generateReport(params);

  if (statusCode.ok(response)) {
    const report = await response.json();
    return { report, hasError: false };
  }

  return { report: null, hasError: true };
});

export const refreshReport = createAction('refreshReport');
