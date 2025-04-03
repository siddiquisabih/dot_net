import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class YourServiceNameService {
  apiName = 'YourServiceName';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/YourServiceName/sample' },
      { apiName: this.apiName }
    );
  }
}
