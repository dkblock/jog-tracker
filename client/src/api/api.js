import config from '../config';
import qs from 'qs';

const buildUrl = (path, params = null) => {
  const url = `${config.baseUrl}/${path}`;
  return params ? `${url}?${qs.stringify(params)}` : url;
};

const api = {
  authenticate: () => buildUrl('api/auth'),
  refresh: () => buildUrl('api/auth/refresh'),

  register: () => buildUrl('api/account/register'),
  login: () => buildUrl('api/account/login'),
  logout: () => buildUrl('api/account/logout'),

  fetchJogs: (params) => buildUrl('api/jogs', params),
  createJog: () => buildUrl('api/jogs'),
  deleteJog: (id) => buildUrl(`api/jogs/${id}`),
  updateJog: (id) => buildUrl(`api/jogs/${id}`),

  generateReport: (params) => buildUrl('api/reports/generate', params),

  fetchUsers: (params) => buildUrl('api/users', params),
  deleteUser: (id) => buildUrl(`api/users/${id}`),
  updateUser: (id) => buildUrl(`api/users/${id}`),
};

export default api;
