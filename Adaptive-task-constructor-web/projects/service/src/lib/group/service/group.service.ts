import { Injectable } from "@angular/core";
import { BehaviorSubject, Subject, take, tap } from "rxjs";
import { Group } from "../entity/group";
import { GroupRepository } from "../repo/group.repository";

@Injectable({providedIn: 'root'})
export class GroupService {
    groupList = new BehaviorSubject<Group[]>(null);
    selectedGroup = new BehaviorSubject<Group>(null);

    constructor(private groupRepository: GroupRepository) {}

    saveGroup(group: Group) {
        if(group.id == 0){
            return this.groupRepository.createGroup(group)
                .pipe(take(1), 
                    tap(g => {
                        this.selectedGroup.next(g);
                    }));
        } else {
            return this.groupRepository.updateGroup(group)
                .pipe(take(1),
                    tap(g => {
                        this.selectedGroup.next(g);
                    }));
        }
    }

    getGroupList(){
        return this.groupRepository.getCurrentUserGroups()
            .pipe(take(1),
                tap(g => {
                    this.groupList.next(g);
                }));
    }

    getFullGroup(id: number){
        return this.groupRepository.getFullGroup(id)
                .pipe(take(1),
                    tap(g => {
                        this.selectedGroup.next(g);
                    }));
    }

    deleteGroup(id: number): any{
        return this.groupRepository.delete(id);
    }

    unselect(){
        this.selectedGroup.next(null);
    }
}