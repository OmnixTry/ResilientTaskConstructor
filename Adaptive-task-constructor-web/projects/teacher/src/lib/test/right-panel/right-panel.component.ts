import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GroupTest } from 'projects/service/src/lib/group/entity/groupTest';
import { Test } from 'projects/service/src/lib/test/entity/test';
import { TestTask } from 'projects/service/src/lib/test/entity/testTask';
import { TestService } from 'projects/service/src/lib/test/service/test.service';
import { take } from 'rxjs';

@Component({
  selector: 'right-panel',
  templateUrl: './right-panel.component.html',
  styleUrls: ['./right-panel.component.scss']
})
export class RightPanelComponent implements OnInit {
  selectedTest: GroupTest;
  fullTest: Test;
  @Input() test: Test;
  @Output() onExportTask = new EventEmitter<TestTask>();


  constructor(private testService: TestService) { }

  ngOnInit(): void {
  }
  onTestSelected(test: GroupTest) {
    if (test) {
      this.selectedTest = test;
      this.testService.getFullTest(test.name).pipe(take(1))
        .subscribe(t => this.fullTest = t);
    }

  }

  onUnselect() {
    this.fullTest = null;
  }

  onExportClick(task: TestTask) {
    this.onExportTask.emit(task);
  }
}
