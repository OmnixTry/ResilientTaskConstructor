import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GroupUser } from 'projects/service/src/lib/group/entity/groupUser';
import { notIncludeValidator } from 'projects/shared/src/lib/validators/notInclude.validator';
import { notSameValidator } from 'projects/shared/src/lib/validators/same.validator';
import { debounceTime, Observable, skipWhile, switchMap } from 'rxjs';
import { SearchService } from '../search.service';

@Component({
  selector: 'lib-add-user-dialog',
  templateUrl: './add-user-dialog.component.html',
  styleUrls: ['./add-user-dialog.component.scss']
})
export class AddUserDialogComponent implements OnInit {
  inputEmail: string;
  selectedUser: GroupUser;
  userList: Observable<GroupUser[]>
  
  emailInput = new FormControl('');
  userId = new FormControl('', notIncludeValidator(this.data.map(d => d.id)));

  constructor(public searchService: SearchService, 
    private dialogRef: MatDialogRef<AddUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: GroupUser[]) {
      console.log(data);
     }


  ngOnInit(): void {
    this.userList = this.emailInput.valueChanges.pipe(skipWhile(v => (v as string).length <=3),
        debounceTime(300),
        switchMap(v => this.searchService.findUser(v)));    
  }

  onUserSelected(group: GroupUser) {
    this.selectedUser = group;
    this.userId.setValue(group.id);
  }
  
  onCancel() {
    this.dialogRef.close();
  }
}
