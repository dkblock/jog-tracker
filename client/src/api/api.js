import config from '../config';
import qs from 'qs';

const buildUrl = (path, params = null) => {
  const url = `${config.baseUrl}/${path}`;
  return params ? `${url}?${qs.stringify(params)}` : url;
};

const api = {
  authenticate: () => buildUrl('api/auth'),
  refresh: () => buildUrl('api/auth/refresh'),

  login: () => buildUrl('api/account/login'),
  register: () => buildUrl('api/account/register'),

  fetchJogs: (params) => buildUrl('api/jogs', params),
  jog: (id) => buildUrl(`api/jogs/${id}`),

  users: () => buildUrl('api/users'),
};

export default api;
