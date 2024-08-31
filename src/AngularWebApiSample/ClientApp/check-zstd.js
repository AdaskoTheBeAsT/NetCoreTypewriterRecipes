const { exec } = require('child_process');
const os = require('os');

const checkZstd = () => {
  exec('zstd --version', (error) => {
    if (error) {
      console.error(
        'Zstd CLI is not installed. Please install it before proceeding:',
      );
      if (os.platform() === 'darwin') {
        console.error('For macOS: brew install zstd');
      } else if (os.platform() === 'linux') {
        console.error('For Linux: sudo apt-get install zstd');
      } else if (os.platform() === 'win32') {
        console.error(
          'For Windows: choco install zstandard or download from https://github.com/facebook/zstd/releases',
        );
      } else {
        console.error(
          'Unsupported OS. Please refer to the Zstandard documentation for installation instructions.',
        );
      }
      process.exit(1);
    } else {
      console.log('Zstd CLI is installed.');
    }
  });
};

checkZstd();
