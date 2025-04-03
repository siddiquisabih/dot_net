import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'YourServiceName',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44320/',
    redirectUri: baseUrl,
    clientId: 'YourServiceName_App',
    responseType: 'code',
    scope: 'offline_access YourServiceName',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44320',
      rootNamespace: 'Tatweer.YourServiceName',
    },
    YourServiceName: {
      url: 'https://localhost:44374',
      rootNamespace: 'Tatweer.YourServiceName',
    },
  },
} as Environment;
