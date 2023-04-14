import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { TestFormService } from 'projects/service/src/lib/test/service/test-form-service';

@Component({
  selector: 'quizz-task',
  templateUrl: './quizz-task.component.html',
  styleUrls: ['./quizz-task.component.scss']
})
export class QuizzTaskComponent implements OnInit {

  @Input() taskForm: FormGroup;

  get options() {
    return (this.taskForm.get('taskOptions') as FormArray);
  }
  constructor(private testFormService: TestFormService) { }

  ngOnInit() {
  }

}
