import api from '../api';
import baseService from './base-service';

const jogsService = {
  fetchJogs: async (searchText, pageIndex, pageSize, sortBy, sortDirection) => {
    const params = {
      searchText,
      pageIndex,
      pageSize,
      sortBy,
      desc: sortDirection === 'desc',
    };

    const url = api.fetchJogs(params);
    return await baseService.get(url);
  },
};

export default jogsService;
