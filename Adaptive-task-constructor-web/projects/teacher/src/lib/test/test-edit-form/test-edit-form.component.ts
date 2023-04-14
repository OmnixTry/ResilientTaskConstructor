import { Component, Inject, Input, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { RegistrationFromComponent } from 'projects/auth/src/lib/components/registration-from/registration-from.component';
import { Test } from 'projects/service/src/lib/test/entity/test';
import { TestTask } from 'projects/service/src/lib/test/entity/testTask';
import { TestFormService } from 'projects/service/src/lib/test/service/test-form-service';
import { TestService } from 'projects/service/src/lib/test/service/test.service';
import { of, switchMap } from 'rxjs';

@Component({
  selector: 'test-edit-form',
  templateUrl: './test-edit-form.component.html',
  styleUrls: ['./test-edit-form.component.scss']
})
export class TestEditFormComponent implements OnInit {
  @Input() testForm: FormGroup;

  get tasks(): FormGroup[] {
    return (this.testForm.get('tasks') as FormArray).controls as FormGroup[];
  }

  constructor(private testService: TestService, private testFormService: TestFormService, private route: ActivatedRoute) { }

  ngOnInit(): void {

  }

  onAddTaskClick() {
    this.testFormService.addEmptyTask(this.testForm);
  }

  onSaveClick() {
    this.testService.updateTest(this.testForm.value).subscribe(t => console.log(t));
  }
}
