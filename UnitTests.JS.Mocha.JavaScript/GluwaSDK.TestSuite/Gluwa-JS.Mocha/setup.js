const GluwaJS = require('');
require ('').config();

const GluwaConfig = {
    production: {
        APIKey: ''
        APISecret: '',
        WebhookSecret: '',
        MasterEthereumAddress: '',
        MasterEthereumPrivateKey: '',
        isDev: false,
    },
    sandbox: {
        APIKey: '',
        APISecret: '',
        WebhookSecret: '',
        MasterEthereumAddress: '',
        MasterEthereumPrivateKey: '',
        isDev: true,
    }
}

var Gluwa = new GluwaJS(GluwaConfig.sandbox);

function setEnvironment(isSandbox) {
    if (!isSandbox) {
        Gluwa = new GluwaJS(GluwaConfig.production);
    }
}

module.exports = { Gluwa, setEnvironment }