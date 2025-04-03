import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { YourServiceNameComponent } from './components/your-service-name.component';
import { YourServiceNameService } from '@tatweer/your-service-name';
import { of } from 'rxjs';

describe('YourServiceNameComponent', () => {
  let component: YourServiceNameComponent;
  let fixture: ComponentFixture<YourServiceNameComponent>;
  const mockYourServiceNameService = jasmine.createSpyObj('YourServiceNameService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [YourServiceNameComponent],
      providers: [
        {
          provide: YourServiceNameService,
          useValue: mockYourServiceNameService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YourServiceNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
