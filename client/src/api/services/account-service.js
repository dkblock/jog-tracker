import api from '../api';
import baseService from './base-service';
import { authenticate } from './utils';

const accountService = {
  authenticate,

  register: async (credentials) => {
    const url = api.register();
    return await baseService.post(url, credentials);
  },

  login: async (credentials) => {
    const url = api.login();
    return await baseService.post(url, credentials);
  },

  logout: async () => {},
};

export default accountService;
