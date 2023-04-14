import { HttpClient } from "@angular/common/http"
import { Injectable } from "@angular/core"
import { BeUrlService } from "projects/shared/src/lib/services/be-url.service"
import { HttpRequestService } from "projects/shared/src/public-api"
import { Observable } from "rxjs"
import { Group } from "../entity/group"

@Injectable({providedIn: 'root'})
export class GroupRepository extends HttpRequestService {
    constructor(http: HttpClient, beUrlService: BeUrlService){
        super(http, beUrlService)
    }

    getCurrentUserGroups(): Observable<Group[]> {
        return this.makeAuthenticatedGet<Group[]>('api/group');
    }

    getFullGroup(id: number): Observable<Group> {
        return this.makeAuthenticatedGet<Group>(`api/group/${id}/full`);
    }

    createGroup(group: Group) {
        return this.makeAuthenticatedPost<Group>('api/group', group);
    }

    updateGroup(group: Group) {
        return this.makeAuthenticatedPut<Group>('api/group', group);
    }

    delete(id: number) {
        return this.makeAuthenticatedDelete<Group>(`api/group/${id}`);
    }
}