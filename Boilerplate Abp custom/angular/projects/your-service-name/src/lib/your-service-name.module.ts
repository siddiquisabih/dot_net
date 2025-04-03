import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { YourServiceNameComponent } from './components/your-service-name.component';
import { YourServiceNameRoutingModule } from './your-service-name-routing.module';

@NgModule({
  declarations: [YourServiceNameComponent],
  imports: [CoreModule, ThemeSharedModule, YourServiceNameRoutingModule],
  exports: [YourServiceNameComponent],
})
export class YourServiceNameModule {
  static forChild(): ModuleWithProviders<YourServiceNameModule> {
    return {
      ngModule: YourServiceNameModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<YourServiceNameModule> {
    return new LazyModuleFactory(YourServiceNameModule.forChild());
  }
}
