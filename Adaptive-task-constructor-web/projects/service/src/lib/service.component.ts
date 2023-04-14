import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'lib-service',
  template: `
    <p>
      service works!
    </p>
  `,
  styles: [
  ]
})
export class ServiceComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
