import { TestBed } from '@angular/core/testing';
import { YourServiceNameService } from './services/your-service-name.service';
import { RestService } from '@abp/ng.core';

describe('YourServiceNameService', () => {
  let service: YourServiceNameService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(YourServiceNameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
