import { ModuleWithProviders, NgModule } from '@angular/core';
import { YOUR_SERVICE_NAME_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class YourServiceNameConfigModule {
  static forRoot(): ModuleWithProviders<YourServiceNameConfigModule> {
    return {
      ngModule: YourServiceNameConfigModule,
      providers: [YOUR_SERVICE_NAME_ROUTE_PROVIDERS],
    };
  }
}
