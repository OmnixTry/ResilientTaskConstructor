import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { TestFormService } from 'projects/service/src/lib/test/service/test-form-service';

@Component({
  selector: 'task-options',
  templateUrl: './task-options.component.html',
  styleUrls: ['./task-options.component.scss']
})
export class TaskOptionsComponent implements OnInit {
  @Input() answersForm: FormArray;
  @Input() isOpenTask: boolean;

  get answers(): FormGroup[] {
    return this.answersForm.controls as FormGroup[];
  }

  get optionsControls() {
    return this.answers.filter((c, i) => !this.isOpenTask || c.get('correct').value);
  }

  get areNoCorrect() {
    return this.answersForm?.errors ? this.answersForm?.errors['notOne'] : null;
  }

  constructor(private testFormService: TestFormService) { }

  ngOnInit() {
  }

  onOptionAdd() {
    this.testFormService.addEmptyOption(this.answersForm);
  }

  onDeleteClick(index: number) {
    this.answersForm.removeAt(index);
  }
}
