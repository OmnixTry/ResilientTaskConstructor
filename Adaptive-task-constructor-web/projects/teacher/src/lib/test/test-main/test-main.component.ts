import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupTest } from 'projects/service/src/lib/group/entity/groupTest';
import { TestService } from 'projects/service/src/lib/test/service/test.service';
import { SearchService } from '../../group/search.service';

@Component({
  selector: 'lib-test-main',
  templateUrl: './test-main.component.html',
  styleUrls: ['./test-main.component.scss']
})
export class TestMainComponent implements OnInit {
  selectedTest: GroupTest;

  constructor(private testService: TestService, private searchService: SearchService, private router: Router) { }

  ngOnInit(): void {
  }

  onTestSelected(test: GroupTest) {
    this.selectedTest = test;
  }

  onEditClick() {

    this.router.navigate([`/teacher/test/new`], { queryParams: { testName: this.selectedTest.name } });
  }

  onDeleteClick() {
    this.testService.deleteTest(this.selectedTest.name)
      .subscribe(_ => this.searchService.refresh());
  }
}
