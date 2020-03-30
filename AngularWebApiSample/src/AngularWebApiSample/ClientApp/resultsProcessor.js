module.exports = function() {
  return require('jest-nunit-reporter').apply(this, arguments);
  // add any other processor you need
};
