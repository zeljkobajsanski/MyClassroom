module.exports = {
  devServer: {
      proxy: {
          'api/': {
              target: 'http://localhost:56079',
              changeOrigin: true
          }
      }
  }
};
