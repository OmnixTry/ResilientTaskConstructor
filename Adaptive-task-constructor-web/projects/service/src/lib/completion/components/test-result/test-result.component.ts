import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Test } from '../../../test/entity/test';
import { TestService } from '../../../test/service/test.service';
import { CompletionFormService } from '../../service/completion-form.service';
import { TestCompletionService } from '../../service/testCompletion.service';
import { switchMap, tap } from 'rxjs';
import { Attempt } from '../../entity/attempt';
import { RoleService } from 'projects/auth/src/lib/services/role.service';
import { AttemptTask } from '../../entity/attemptTask';

@Component({
  selector: 'srvc-test-result',
  templateUrl: './test-result.component.html',
  styleUrls: ['./test-result.component.scss']
})
export class TestResultComponent implements OnInit {
  attemptId: number;
  testId: number;
  test: Test;
  attemptFrom: FormGroup;
  attempt: Attempt;

  isReadMode: boolean;

  get maxScore() {
    return this.test?.tasks.map(t => t.score).reduce((a, b) => a + b, 0);
  }

  get curretScore() {
    return (this.attemptFrom.get('tasks').value as AttemptTask[]).map(t => Number.parseInt(t.score as string)).reduce((a, b) => a + b, 0);
  }

  constructor(private testService: TestService,
    private route: ActivatedRoute,
    private formService: CompletionFormService,
    private completionService: TestCompletionService,
    private roleServeice: RoleService) { }

  ngOnInit(): void {
    const querryParamMap = this.route.queryParamMap
      .pipe(
        switchMap(map => {
          this.attemptId = Number.parseInt(map.get('attemptId'));
          return this.completionService.getFullAttempt(this.attemptId);
        }),
        switchMap(attempt => {
          this.attempt = attempt;
          return this.testService.getMysteryTest(attempt.testId);
        }),
        tap(test => {
          this.isReadMode = this.roleServeice.getRole() != 'Teacher';
          this.attemptFrom = this.formService.createFormFromAttempt(this.attempt, test, this.isReadMode);
          this.test = test;

          //this.attemptFrom = this.formService.createForm(test);
          //this.attemptFrom.valueChanges.subscribe(v => console.log(v));
          //this.test = test;
        })
      )
      .subscribe();
  }

  getAttemptTaskForm(taskId: number) {
    return (this.attemptFrom?.get('tasks') as FormArray)?.controls.find((t) => t.value.taskId == taskId) as FormGroup;//.get('score');
  }

  getTaskAnswers(taskId: number) {
    return this.attemptFrom.value.tasks.find((t: any) => t.taskId == taskId).answers;
  }

  getOption(taskId: number, optionId: number) {
    return this.test.tasks.find(t => t.id == taskId).taskOptions.find(o => o.id == optionId);
  }

  getMaxScoreByAnswer(taskId: number): number {
    return this.test.tasks.find(t => t.id == taskId).score;
  }

  onSaveClick() {
    this.completionService
      .saveManualCheck(this.attemptFrom.value)
      .subscribe(a => {
        this.attemptFrom = this.formService.createFormFromAttempt(a, this.test, this.isReadMode);
        this.attempt = a;
      });
  }
}
