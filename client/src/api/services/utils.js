import api from '../api';
import statusCode from '../../utils/status-code-reader';
import { isJwtExpired } from '../../utils/jwt';
import {
  clearAccessToken,
  clearRefreshToken,
  getAccessToken,
  getRefreshToken,
  setAccessToken,
  setRefreshToken,
} from '../../utils/local-storage-manager';

export const refreshJwtIfNeed = async () => {
  const accessToken = getAccessToken();

  if (!accessToken) {
    return;
  }

  if (!isJwtExpired(accessToken)) {
    return;
  }

  await refreshJwt();
};

const refreshJwt = async () => {
  try {
    const accessToken = getAccessToken();
    const refreshToken = getRefreshToken();

    if (!accessToken || !refreshToken) {
      return;
    }

    const response = await fetch(api.refresh(), {
      method: 'POST',
      body: JSON.stringify({ jwt: { accessToken, refreshToken } }),
      headers: { 'Content-Type': 'application/json' },
    });

    if (statusCode.ok(response)) {
      const { accessToken, refreshToken } = await response.json();
      setAccessToken(accessToken);
      setRefreshToken(refreshToken);
    } else {
      clearAccessToken();
      clearRefreshToken();
    }
  } catch (e) {
    console.log(e);
  }
};
