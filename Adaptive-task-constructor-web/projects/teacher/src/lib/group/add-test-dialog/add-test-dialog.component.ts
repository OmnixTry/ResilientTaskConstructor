import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GroupTest } from 'projects/service/src/lib/group/entity/groupTest';
import { GroupUser } from 'projects/service/src/lib/group/entity/groupUser';
import { notIncludeValidator } from 'projects/shared/src/lib/validators/notInclude.validator';
import { AddUserDialogComponent } from '../add-user-dialog/add-user-dialog.component';
import { SearchService } from '../search.service';

@Component({
  selector: 'lib-add-test-dialog',
  templateUrl: './add-test-dialog.component.html',
  styleUrls: ['./add-test-dialog.component.css']
})
export class AddTestDialogComponent implements OnInit {
  selectedTest: GroupTest;
  testForm = new FormControl('', notIncludeValidator(this.data.map(g => g.id)))

  constructor(public searchService: SearchService, 
    private dialogRef: MatDialogRef<AddUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: GroupTest[]) { }

  ngOnInit(): void {
  }

  onTestSelected(test: GroupTest) {
    this.selectedTest = test;
    this.testForm.setValue(test.id);
  }

  onCancel() {
    this.dialogRef.close();
  }
}
