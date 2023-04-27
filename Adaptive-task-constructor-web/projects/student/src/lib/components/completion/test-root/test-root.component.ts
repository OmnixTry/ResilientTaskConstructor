import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CompletionFormService } from 'projects/service/src/lib/completion/service/completion-form.service';
import { TestCompletionService } from 'projects/service/src/lib/completion/service/testCompletion.service';
import { Test } from 'projects/service/src/lib/test/entity/test';
import { TestService } from 'projects/service/src/lib/test/service/test.service';
import { switchMap, tap, combineLatest } from 'rxjs/operators';
import { Buffer } from 'buffer';
import { AuthenticationService } from 'projects/auth/src/lib/services/authentication.service';
import { timer } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'lib-test-root',
  templateUrl: './test-root.component.html',
  styleUrls: ['./test-root.component.scss']
})
export class TestRootComponent implements OnInit {
  testId: number;
  test: Test;
  attemptFrom: FormGroup;

  ddosQuantityFormControl = new FormControl(0);

  constructor(private testService: TestService,
    private route: ActivatedRoute,
    private formService: CompletionFormService,
    private completionService: TestCompletionService,
    private router: Router) { }

  ngOnInit() {
    const querryParamMap = this.route.queryParamMap
      .pipe(
        switchMap(map => {
          this.testId = Number.parseInt(map.get('testId'));
          return this.testService.getMysteryTest(this.testId)
        }),
        tap(test => {
          this.attemptFrom = this.formService.createForm(test);
          this.attemptFrom.valueChanges.subscribe(v => console.log(v));
          this.test = test;
        })
      )
      .subscribe();
  }

  getTask(index: number) {
    const x = (this.attemptFrom.get('tasks') as FormArray);
    const y = (this.attemptFrom.get('tasks') as FormArray).controls[index] as FormGroup;
    return (this.attemptFrom.get('tasks') as FormArray).controls[index] as FormGroup;
  }

  onSubmit() {
    this.completionService.checkTest(this.attemptFrom.value).subscribe(v => {
      this.router.navigate(['/student/result'], { queryParams: { attemptId: v.id } })
      console.log(v);
    });
  }

  async onDebugQueue() {
    var value = +this.ddosQuantityFormControl.value;
    console.log(+this.ddosQuantityFormControl.value)
    for (let index = 0; index < value; index++) {
      this.completionService.checkTestAsync(this.attemptFrom.value)
        .subscribe((response: any) => {
          this.router.navigate(['/student']);
          console.log(response);
        });
      await timer(80).pipe(take(1)).toPromise();
    }
    // this.completionService.checkTestAsync(this.attemptFrom.value)
    //   .subscribe((response: any) => {
    //     this.router.navigate(['/student']);
    //     console.log(response);
    //   });
  }
}
