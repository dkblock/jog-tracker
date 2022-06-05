import api from '../api';
import baseService from './base-service';

const reportsService = {
  generateReport: async (params) => {
    const url = api.generateReport(params);
    return await baseService.get(url);
  },
};

export default reportsService;