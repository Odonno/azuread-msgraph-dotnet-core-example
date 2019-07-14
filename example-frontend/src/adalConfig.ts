import { AuthenticationContext, AdalConfig } from 'react-adal';

export const adalConfig: AdalConfig = {
    tenant: '<YOUR_TENANT>',
    clientId: '<YOUR_CLIENT_ID>',
    redirectUri: 'YOUR_REDIRECT_URI',
    endpoints: {
        api: '<YOUR_API_ENDPOINT>',
    },
    cacheLocation: 'sessionStorage'
};

export const authContext = new AuthenticationContext(adalConfig);

export const getToken = () => authContext.getCachedToken(adalConfig.clientId);