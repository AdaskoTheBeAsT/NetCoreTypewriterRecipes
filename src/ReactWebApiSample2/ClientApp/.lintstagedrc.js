module.exports = {
  '{apps,libs}/**/*.html,!**/api/**': ['htmlhint'],
  '{apps,libs}/**/*.{css,scss},!**/api/**': ['stylelint --fix'],
  '{apps,libs}/**/*.{js,jsx,ts,tsx},!**/api/**': ['eslint --fix'],
  '{apps,libs}/**/*.{js,jsx,ts,tsx,css,scss,md,html,json},!**/api/**': [
    'prettier --write',
  ],
};
