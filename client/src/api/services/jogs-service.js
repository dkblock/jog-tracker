import api from '../api';
import baseService from './base-service';

const jogsService = {
  fetchJogs: async (params) => {
    const searchParams = { ...params, onlyOwn: params.showOnlyOwn, desc: params.sortDirection === 'desc' };

    const url = api.fetchJogs(searchParams);
    return await baseService.get(url);
  },

  createJog: async (jog) => {
    const url = api.createJog();
    return await baseService.post(url, jog);
  },

  deleteJog: async (jogId) => {
    const url = api.deleteJog(jogId);
    return await baseService.delete(url);
  },

  updateJog: async (jog) => {
    const url = api.updateJog(jog.id);
    return await baseService.put(url, jog);
  },
};

export default jogsService;
