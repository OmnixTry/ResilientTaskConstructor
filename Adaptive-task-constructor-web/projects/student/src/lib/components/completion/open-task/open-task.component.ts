import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { CompletionFormService } from 'projects/service/src/lib/completion/service/completion-form.service';
import { TestTask } from 'projects/service/src/lib/test/entity/testTask';

@Component({
  selector: 'open-task',
  templateUrl: './open-task.component.html',
  styleUrls: ['./open-task.component.scss']
})
export class OpenTaskComponent implements OnInit {
  @Input() taskForm: FormGroup;
  @Input() task: TestTask;

  get answer(): FormGroup {
    return (this.taskForm.get('answers') as FormArray).controls[0] as FormGroup;
  }
  constructor(private completionFormService: CompletionFormService) { }

  ngOnInit(): void {
    const answers = this.taskForm.get('answers') as FormArray;
    if (answers.length == 0) {
      this.completionFormService.addTextAnswer(this.taskForm);
    }
  }

}
