import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Group } from 'projects/service/src/lib/group/entity/group';

@Component({
  selector: 'lib-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.css']
})
export class CreateGroupComponent implements OnInit {
  group: Group = {
    id: 0,
    name: 'New Group',
	  teacherId: '',
	  teacher: null,
	  tests: [],
	  students: [],
  }

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onSave() {
    this.router.navigate(['/teacher/group']);
  }

}
