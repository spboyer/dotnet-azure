const MsRest = require("ms-rest-azure");

module.exports = function(callback) {
  MsRest.interactiveLogin((err, credentials) => {
    var result = {};
    if (err) throw err;

    result.clientId = credentials.tokenCache._entries[0]._clientId;
    result.tenantId = credentials.tokenCache._entries[0].tenantId;
    result.environment = credentials.environment.name;
    result.managementEndpointUrl =
      credentials.environment.managementEndpointUrl;
    result.resourceManagerEndpointUrl =
      credentials.environment.resourceManagerEndpointUrl;

    result.token = credentials.tokenCache._entries[0].accessToken;
    result.expiresOn = credentials.tokenCache._entries[0].expiresOn;

    //result.raw = credentials;

    callback(null, JSON.stringify(result));
  });
};
