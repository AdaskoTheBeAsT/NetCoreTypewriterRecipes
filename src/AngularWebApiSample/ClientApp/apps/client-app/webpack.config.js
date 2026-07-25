/* eslint-disable @typescript-eslint/no-require-imports */
const { merge } = require('webpack-merge');
const CompressionPlugin = require('compression-webpack-plugin');
const zlib = require('node:zlib');
const sharp = require('sharp'); // Import sharp for image processing

// env-config*.js is rewritten by env.sh at container startup, so it must stay uncompressed.
const skipRuntimeConfig = /env-config(.*)\.js$/;

// Zstandard through Node's zlib (Node.js 22.15+ / 24+), no zstd CLI required.
const zstdPlugins =
  typeof zlib.zstdCompress === 'function'
    ? [
        new CompressionPlugin({
          filename: '[path][base].zst',
          algorithm(input, options, callback) {
            zlib.zstdCompress(
              input,
              {
                params: {
                  [zlib.constants.ZSTD_c_compressionLevel]: options.level ?? 19,
                },
              },
              callback,
            );
          },
          deleteOriginalAssets: false,
          exclude: skipRuntimeConfig,
          test: /\.(js|css|html|svg|ttf)$/,
          threshold: 1024,
          minRatio: 0.8,
          compressionOptions: {
            level: 19, // Zstandard compression level (1-22)
          },
        }),
      ]
    : [];

// eslint-disable-next-line @typescript-eslint/no-unused-vars
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
      ...zstdPlugins,
      // AVIF Image Compression
      new CompressionPlugin({
        //'[path][name].avif',
        filename: '[path][name].avif',
        algorithm(input, options, callback) {
          sharp(input)
            .avif({ quality: 50 }) // Adjust quality as needed (0-100)
            .toBuffer((err, outputBuffer) => {
              if (err) {
                return callback(err);
              }
              callback(null, outputBuffer);
            });
        },
        deleteOriginalAssets: false,
        test: /\.[^.]+\.(png|jpg|jpeg)$/, // Apply only to image files
        threshold: 1024,
        minRatio: 0.8,
      }),
      // WebP Image Compression
      new CompressionPlugin({
        //'[path][name].webp',
        filename: '[path][name].webp',
        algorithm(input, options, callback) {
          sharp(input)
            .webp({ quality: 75 }) // Adjust quality as needed (0-100)
            .toBuffer((err, outputBuffer) => {
              if (err) {
                return callback(err);
              }
              callback(null, outputBuffer);
            });
        },
        deleteOriginalAssets: false,
        test: /\.[^.]+\.(png|jpg|jpeg)$/, // Apply only to image files
        threshold: 1024,
        minRatio: 0.8,
      }),
    ],
  });
};
