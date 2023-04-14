import { outputAst } from '@angular/compiler';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Group } from '../../entity/group';
import { GroupService } from '../../service/group.service';

@Component({
  selector: 'srvc-group-list',
  templateUrl: './group-list.component.html',
  styleUrls: ['./group-list.component.scss']
})
export class GroupListComponent implements OnInit {
  @Output() groupSelect = new EventEmitter<Group>();

  constructor(public groupService: GroupService) { }

  ngOnInit(): void {
    this.groupService.getGroupList().subscribe();
  }

  onGroupSelect(group: Group){
    console.log(group);
    //this.groupService.selectedGroup.next(group);
    this.groupSelect.emit(group);
  }
}
