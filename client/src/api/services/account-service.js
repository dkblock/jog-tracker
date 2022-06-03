import api from '../api';
import baseService from './base-service';

const accountService = {
  register: async (credentials) => {
    const url = api.register();
    return await baseService.post(url, credentials);
  },

  login: async (credentials) => {
    const url = api.login();
    return await baseService.post(url, credentials);
  },
};

export default accountService;
