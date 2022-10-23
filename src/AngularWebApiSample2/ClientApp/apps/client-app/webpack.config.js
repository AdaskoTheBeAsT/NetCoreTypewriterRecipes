const { merge } = require('webpack-merge');
const CompressionPlugin = require('compression-webpack-plugin');
const zlib = require('zlib');

module.exports = (config, context) => {
  return merge(config, {
    plugins: [
      new CompressionPlugin({
        algorithm: 'gzip',
        deleteOriginalAssets: false,
        test: /\.(js|css|html|svg|ttf)$/,
        threshold: 1024,
        minRatio: 0.8,
        compressionOptions: {
          level: 9,
        },
      }),
      new CompressionPlugin({
        filename: '[path][base].br',
        algorithm: 'brotliCompress',
        deleteOriginalAssets: false,
        test: /\.(js|css|html|svg|ttf)$/,
        threshold: 1024,
        minRatio: 0.8,
        compressionOptions: {
          params: {
            [zlib.constants.BROTLI_PARAM_QUALITY]: 11,
          },
        },
      }),
    ],
  });
};
