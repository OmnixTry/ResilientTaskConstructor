import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TestCompletionService } from 'projects/service/src/lib/completion/service/testCompletion.service';
import { GroupTest } from 'projects/service/src/lib/group/entity/groupTest';
import { GroupService } from 'projects/service/src/lib/group/service/group.service';
import { TestService } from 'projects/service/src/lib/test/service/test.service';
import { SearchService } from 'projects/teacher/src/lib/group/search.service';

@Component({
  selector: 'lib-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  selectedGroup = this.groupService.selectedGroup;
  groupList = this.groupService.groupList;

  displayColumns = ['name', 'start', 'results'];

  constructor(private testService: TestService,
    private searchService: SearchService,
    private router: Router,
    private completionService: TestCompletionService,
    private groupService: GroupService) { }


  ngOnInit(): void {
    this.groupService.getGroupList().subscribe();
    this.groupService.unselect();
  }

  onGroupSelected(test: GroupTest) {
    this.groupService.getFullGroup(test.id).subscribe();
  }

  onEditClick() {
  }

  onDeleteClick() {
    this.testService.deleteTest(this.selectedGroup.value.name)
      .subscribe(_ => this.searchService.refresh());
  }

  onTakeTestClick(test: GroupTest) {
    const queryParams =
      this.router.navigate(['/student/takeTest/'], { queryParams: { testId: test.id } });
  }

  onViewResultsClick(test: GroupTest) {
    const queryParams =
      this.router.navigate(['/student/result-list/'], { queryParams: { testId: test.id } });
  }

}
