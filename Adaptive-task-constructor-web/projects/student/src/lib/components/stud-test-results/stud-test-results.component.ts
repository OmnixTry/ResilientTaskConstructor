import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Attempt } from 'projects/service/src/lib/completion/entity/attempt';
import { TestCompletionService } from 'projects/service/src/lib/completion/service/testCompletion.service';
import { switchMap, tap } from 'rxjs';


@Component({
  selector: 'lib-stud-test-results',
  templateUrl: './stud-test-results.component.html',
  styleUrls: ['./stud-test-results.component.scss']
})
export class StudTestResultsComponent implements OnInit {
  testId: number;
  studentId: string;
  testResults: Attempt[];
  displayResults: any;


  constructor(private testCompletionService: TestCompletionService, private route: ActivatedRoute, private router: Router, private datePipe: DatePipe) { }

  ngOnInit(): void {
    const querryParamMap = this.route.queryParamMap
      .pipe(
        switchMap(map => {
          this.testId = Number.parseInt(map.get('testId'));
          this.studentId = map.get('studentId');
          if (!this.studentId) {
            return this.testCompletionService.getCurrentStudentResults(this.testId);
          }
          else {
            return this.testCompletionService.getStudentResults(this.testId, this.studentId);
          }
        }),
        tap(tests => {
          this.testResults = tests;
          this.displayResults = this.testResults?.map(t => { return { ...t, date: this.datePipe.transform(t.date, "MMM dd, YYYY : HH:mm"), score: t.score + '/' + t.maxScore } });
        })
      )
      .subscribe();
  }

  onSelected(event: Attempt) {
    this.router.navigate(['/student/result'], { queryParams: { attemptId: event.id } });
  }
}


