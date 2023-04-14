import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Test } from 'projects/service/src/lib/test/entity/test';
import { TestTask } from 'projects/service/src/lib/test/entity/testTask';
import { TestFormService } from 'projects/service/src/lib/test/service/test-form-service';
import { TestService } from 'projects/service/src/lib/test/service/test.service';
import { of, switchMap } from 'rxjs';

@Component({
  selector: 'lib-constructor',
  templateUrl: './constructor.component.html',
  styleUrls: ['./constructor.component.scss']
})
export class ConstructorComponent implements OnInit {
  @Input() test: Test;

  testForm: FormGroup;

  constructor(private testService: TestService, private testFormService: TestFormService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let x = this.route.queryParamMap.pipe(
      switchMap(params => {
        const testName = params.get('testName');
        if (!!testName) {
          return this.testService.getFullTest(testName);
        }
        else return of(null);
      })
    ).subscribe(test => {
      if (!!test) {
        this.testForm = this.testFormService.createFormFromObject(test);
      }
      else {
        this.testForm = this.testFormService.createEmptyForm();
      }
      this.testForm.valueChanges.subscribe(t => console.log(t));
    });
  }

  onAddTaskClick() {
    this.testFormService.addEmptyTask(this.testForm);
  }

  onSaveClick() {
    this.testService.updateTest(this.testForm.value).subscribe(t => {
      console.log(t);
      this.testForm = this.testFormService.createFormFromObject(t);
      this.testForm.valueChanges.subscribe(t => console.log(t));
    });
  }

  isSaveDisabled() {
    const tests = (this.testForm?.get('tasks') as FormArray);
    return !!tests && tests.controls.length < 1 || !this.testForm?.valid;
  }

  onExportTask(task: TestTask) {
    this.testFormService.importTask(this.testForm, task);
  }
}
