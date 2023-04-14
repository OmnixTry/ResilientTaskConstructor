import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormGroup } from '@angular/forms';
import { TestTaskType } from 'projects/service/src/lib/test/entity/testType';

@Component({
  selector: 'task-general',
  templateUrl: './task-general.component.html',
  styleUrls: ['./task-general.component.scss']
})
export class TaskGeneralComponent implements OnInit {
  @Input() taskForm: FormGroup;

  taskTypeEnum = TestTaskType;
  get type(){
    return Object.keys(this.taskTypeEnum);
  }
  get taskType() {
    return this.taskForm?.value.type as TestTaskType;
  }

  constructor() { }

  ngOnInit() {
  }

  onRemoveClick() {
    const arr = (this.taskForm.parent as FormArray);
    arr.removeAt(arr.controls.indexOf(this.taskForm));
  }

  get index() {
    const arr = (this.taskForm.parent as FormArray);
    return arr.controls.indexOf(this.taskForm);
  }
}
