import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentResult } from 'projects/service/src/lib/completion/entity/studentResult';
import { TestCompletionService } from 'projects/service/src/lib/completion/service/testCompletion.service';
import { switchMap, tap } from 'rxjs';


@Component({
  selector: 'lib-test-results',
  templateUrl: './test-results.component.html',
  styleUrls: ['./test-results.component.scss']
})
export class TestResultsComponent implements OnInit {
  testId: number;
  groupId: number;

  testResults: StudentResult[];

  constructor(private completionService: TestCompletionService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const querryParamMap = this.route.queryParamMap
      .pipe(
        switchMap(map => {
          this.testId = Number.parseInt(map.get('testId'));
          this.groupId = Number.parseInt(map.get('groupId'));

          return this.completionService.getTestResults(this.testId, this.groupId);
        }),
        tap(tests => {
          this.testResults = tests;
        })
      )
      .subscribe();
  }

  onStudentClick(result: StudentResult) {
    this.router.navigate(['stud-result'], { queryParams: { testId: this.testId, studentId: result.student.id } });
  }
}
