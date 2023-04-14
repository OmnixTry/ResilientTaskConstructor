import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Group } from 'projects/service/src/lib/group/entity/group';
import { GroupService } from 'projects/service/src/lib/group/service/group.service';
import { take } from 'rxjs';

@Component({
  selector: 'teacher-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainGroupComponent implements OnInit {
  form : FormArray;

  constructor(private groupService: GroupService, private router: Router) { }

  ngOnInit(): void {

  }

  onGroupSelect(group: Group){
    if(group){
      this.groupService.getFullGroup(group.id)
      .pipe(take(1))
      .subscribe(g =>console.log(g));
    }
    else {
      this.groupService.unselect();
    }    
  }

  onCreateClick() {
    this.router.navigate(['/teacher/group/new'], { queryParams: { returnUrl: '/teacher/group' }});
  }
}
