export default (() => {
  const config = {
    development: () => require('./config.development.json'),
    production: () => ({
      serverUrl: process.env.SERVER_URL,
    }),
  };

  return config[process.env.NODE_ENV]();
})();
