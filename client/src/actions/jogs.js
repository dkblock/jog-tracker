import { createAsyncThunk } from '@reduxjs/toolkit';

import { jogsService } from '../api';
import statusCode from '../utils/status-code-reader';

export const fetchJogs = createAsyncThunk(
  'fetchJogs',
  async ({ searchText, pageIndex, pageSize, sortBy, sortDirection }) => {
    const response = await jogsService.fetchJogs();

    if (statusCode.ok(response)) {
      const { page: jogs, totalCount } = await response.json();
      return { jogs, totalCount, hasError: false };
    }

    return { jogs: [], totalCount: 0, hasError: true };
  },
);
