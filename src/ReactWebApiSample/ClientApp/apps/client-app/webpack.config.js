const { composePlugins, withNx } = require('@nrwl/webpack');
const { withReact } = require('@nrwl/react');
const { merge } = require('webpack-merge');
const CompressionPlugin = require('compression-webpack-plugin');
const zlib = require('zlib');

// Nx plugins for webpack.
module.exports = composePlugins(
  withNx(),
  withReact(),
  (config, { options, context }) => {
    // Note: This was added by an Nx migration.
    // You should consider inlining the logic into this file.
    // For more information on webpack config and Nx see:
    // https://nx.dev/packages/webpack/documents/webpack-config-setup
    return merge(config, {
      module: {
        rules: [
          {
            test: /\.svg$/,
            use: ['@svgr/webpack', 'url-loader'],
            issuer: {
              and: [/\.(ts|tsx|js|jsx|md|mdx)$/],
            },
          },
        ],
      },
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
  }
);
