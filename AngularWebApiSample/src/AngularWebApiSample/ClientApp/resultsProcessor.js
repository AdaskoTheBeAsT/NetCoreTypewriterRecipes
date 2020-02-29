module.exports = function() {
  require('jest-html-reporter').apply(this, arguments);
  require('jest-sonar-reporter').apply(this, arguments);
  return require('jest-nunit-reporter').apply(this, arguments);
  // add any other processor you need
};
