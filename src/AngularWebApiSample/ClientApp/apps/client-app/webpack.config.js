/* eslint-disable @typescript-eslint/no-require-imports */
const { merge } = require('webpack-merge');
const CompressionPlugin = require('compression-webpack-plugin');
const zlib = require('zlib');
const { exec } = require('child_process');
const fs = require('fs');
const path = require('path');
const os = require('os'); // Correctly import the os module
const sharp = require('sharp'); // Import sharp for image processing
const crypto = require('crypto'); // Import crypto for hash generation

function generateHash(buffer) {
  return crypto.createHash('md5').update(buffer).digest('hex').slice(0, 8);
}

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
      new CompressionPlugin({
        filename: '[path][base].zst',
        algorithm(input, options, callback) {
          // Use system temp directory
          const tempDir = os.tmpdir();
          const tempInputPath = path.join(tempDir, `temp-input-${Date.now()}`);
          const tempOutputPath = path.join(
            tempDir,
            `temp-output-${Date.now()}.zst`,
          );

          // Write the input buffer to a temporary file
          fs.writeFileSync(tempInputPath, input);

          // Run the Zstandard command using exec
          exec(
            `zstd -o ${tempOutputPath} ${tempInputPath} -${options.level || 3}`,
            (err) => {
              if (err) {
                return callback(err);
              }

              // Read the compressed file back into a buffer
              const compressed = fs.readFileSync(tempOutputPath);
              callback(null, compressed);

              // Clean up the temporary files
              try {
                fs.unlinkSync(tempInputPath);
                fs.unlinkSync(tempOutputPath);
              } catch (unlinkErr) {
                console.error('Error cleaning up temp files:', unlinkErr);
              }
            },
          );
        },
        deleteOriginalAssets: false,
        test: /\.(js|css|html|svg|ttf)$/,
        threshold: 1024,
        minRatio: 0.8,
        compressionOptions: {
          level: 22, // Zstandard compression level (1-22)
        },
      }),
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
