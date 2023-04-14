import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { TestFormService } from 'projects/service/src/lib/test/service/test-form-service';

@Component({
  selector: 'open-task',
  templateUrl: './open-task.component.html',
  styleUrls: ['./open-task.component.scss']
})
export class OpenTaskComponent implements OnInit {
  @Input() taskForm: FormGroup;

  get options() {
    return (this.taskForm.get('taskOptions') as FormArray);
  }

  constructor(private testFormService: TestFormService) { }

  ngOnInit() {
  }

}
