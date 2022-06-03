const EXP_LIMIT_30_SEC = 1000 * 30;

export const isJwtExpired = (token) => parseJwt(token).exp * 1000 - Date.now() <= EXP_LIMIT_30_SEC;

export const parseJwt = (token) => {
  const base64Url = token.split('.')[1];
  const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  const jsonPayload = decodeURIComponent(
    atob(base64)
      .split('')
      .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
      .join(''),
  );

  return JSON.parse(jsonPayload);
};
