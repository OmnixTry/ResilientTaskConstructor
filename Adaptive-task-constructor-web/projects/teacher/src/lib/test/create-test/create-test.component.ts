import { Component, OnInit } from '@angular/core';
import { Test } from 'projects/service/src/lib/test/entity/test';
import { TestService } from 'projects/service/src/lib/test/service/test.service';

@Component({
  selector: 'lib-create-test',
  templateUrl: './create-test.component.html',
  styleUrls: ['./create-test.component.css']
})
export class CreateTestComponent implements OnInit {

  constructor(private testService: TestService) { }

  ngOnInit(): void {
  }


}
