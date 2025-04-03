import { Component, inject, OnInit } from '@angular/core';
import { YourServiceNameService } from '../services/your-service-name.service';

@Component({
  standalone: false,
  selector: 'lib-your-service-name',
  template: ` <p>your-service-name works!</p> `,
  styles: [],
})
export class YourServiceNameComponent implements OnInit {
  private service = inject(YourServiceNameService);

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
