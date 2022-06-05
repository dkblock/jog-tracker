import api from '../api';
import baseService from './base-service';

const usersService = {
  fetchUsers: async (params) => {
    const searchParams = { ...params, desc: params.sortDirection === 'desc' };

    const url = api.fetchUsers(searchParams);
    return await baseService.get(url);
  },

  deleteUser: async (userId) => {
    const url = api.deleteUser(userId);
    return await baseService.delete(url);
  },

  updateUser: async (user) => {
    const url = api.updateUser(user.id);
    return await baseService.put(url, user);
  },
};

export default usersService;
