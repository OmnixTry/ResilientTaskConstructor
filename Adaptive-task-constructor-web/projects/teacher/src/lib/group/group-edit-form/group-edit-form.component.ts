import { group } from '@angular/animations';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Form, FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { Group } from 'projects/service/src/lib/group/entity/group';
import { GroupTest } from 'projects/service/src/lib/group/entity/groupTest';
import { GroupUser } from 'projects/service/src/lib/group/entity/groupUser';
import { GroupService } from 'projects/service/src/lib/group/service/group.service';
import { filter, takeUntil } from 'rxjs';
import { Subject } from 'rxjs/internal/Subject';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddUserDialogComponent } from '../add-user-dialog/add-user-dialog.component';
import { AddTestDialogComponent } from '../add-test-dialog/add-test-dialog.component';
import { InvokeFunctionExpr } from '@angular/compiler';
import { ActivatedRoute, Router } from '@angular/router';
import { notIncludeValidator } from 'projects/shared/src/lib/validators/notInclude.validator';
import { TestBed } from '@angular/core/testing';

@Component({
  selector: 'teach-group-edit-form',
  templateUrl: './group-edit-form.component.html',
  styleUrls: ['./group-edit-form.component.scss']
})
export class GroupEditFormComponent implements OnInit, OnDestroy {
  @Input() group: Group = null;
  @Output() save = new EventEmitter<Group>()

  groupForm: FormGroup;
  returnUrl: string;
  groupNameList: string[];
  private onDestroy$ = new Subject<boolean>();


  constructor(private fb: FormBuilder,
    private groupService: GroupService,
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    if (this.group == null) {
      this.groupService.selectedGroup
        .pipe(takeUntil(this.onDestroy$))
        .subscribe((g: Group) => {
          this.createGroupForm(g);
        });
    }
    else {
      this.createGroupForm(this.group);
      this.route.queryParamMap.subscribe(res => {
        return this.returnUrl = res.get("returnUrl");
      });
    }
  }

  ngOnDestroy() {
    this.onDestroy$.next(true);
    this.onDestroy$.complete();
  }

  createGroupForm(group: Group) {
    const listOfNames = this.groupService.groupList.value?.map(g => g.name);// .subscribe(l => this.groupNameList = l.map(g => g.name));

    if (!group) {
      this.groupForm = null;
    }
    else {
      this.groupForm = this.fb.group({
        id: [group.id],
        name: [group.name, notIncludeValidator(listOfNames)],
        teacherId: [group.teacherId],
        students: this.createUserArray(group),
        tests: this.createTestArray(group)
      });

      this.groupForm.valueChanges.subscribe(x => console.log(this.groupForm));
    }
  }

  createUserArray(group: Group): FormArray {
    if (!!group.students) {
      const students = group.students.map((student: GroupUser) => this.createUserControl(student))
      const array = this.fb.array(students);
      return array;
    }
    return this.fb.array([]);
  }

  createTestArray(group: Group): FormArray {
    if (!!group.students) {
      const students = group.tests.map((test) => this.createTestControl(test))
      const array = this.fb.array(students);
      return array;
    }
    return this.fb.array([]);
  }

  onSaveClick() {
    this.save.emit(this.groupForm.value);
    this.groupService.saveGroup(this.groupForm.value).subscribe(x => this.groupService.getGroupList().subscribe());
  }

  onAddTestClick() {
    const userAddDialog = this.dialog.open(AddTestDialogComponent, {
      width: '500px',
      data: this.groupForm.value.tests
    });
    userAddDialog.afterClosed().subscribe(val => {
      console.log(val);
      if (!!val) {
        (this.groupForm.get('tests') as FormArray).push(this.createTestControl(val));
      }
    });
  }

  onAddStudentClick() {
    const userAddDialog = this.dialog.open(AddUserDialogComponent, {
      width: '400px',
      data: this.groupForm.value.students
    });
    userAddDialog.afterClosed().subscribe(val => {
      console.log(val);
      if (!!val) {
        (this.groupForm.get('students') as FormArray).push(this.createUserControl(val));
      }
    })
  }

  onStudentDeleteClick(element: GroupUser) {
    const studentArray = this.groupForm.get('students') as FormArray;
    studentArray.removeAt((studentArray.value as GroupUser[]).findIndex(t => t.id == element.id))
  }

  onStudentResultsClick(element: GroupTest) {
    this.router.navigate(['/teacher/test-results/'], { queryParams: { testId: element.id, groupId: this.groupForm.value.id } });
  }

  onTestDeleteClick(element: GroupTest) {
    const testArray = this.groupForm.get('tests') as FormArray;
    testArray.removeAt((testArray.value as GroupTest[]).findIndex(t => t.id == element.id))
  }

  onReturnClick() {
    this.router.navigate([this.returnUrl]);
  }

  onDeleteClick() {
    this.groupService.deleteGroup(this.groupForm.value.id)
      .subscribe((_: any) => this.groupService.getGroupList().subscribe());
  }

  private createUserControl(student: GroupUser) {
    return this.fb.group({
      id: [student.id],
      firstName: [student.firstName],
      lastName: [student.lastName],
      email: [student.email],
    });
  }

  private createTestControl(test: GroupTest) {
    return this.fb.group({
      id: [test.id],
      name: [test.name],
    })
  }
}
