import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { GroupTest } from "projects/service/src/lib/group/entity/groupTest";
import { GroupUser } from "projects/service/src/lib/group/entity/groupUser";
import { BeUrlService } from "projects/shared/src/lib/services/be-url.service";
import { HttpRequestService } from "projects/shared/src/public-api";
import { BehaviorSubject, Observable, of, Subject } from "rxjs";


@Injectable({ providedIn: 'root' })
export class SearchService extends HttpRequestService {
    refreshSearch: Subject<boolean> = new Subject<boolean>();

    constructor(http: HttpClient, beUrlService: BeUrlService) {
        super(http, beUrlService);
    }

    findUser(name: string): Observable<GroupUser[]> {
        return this.makeAuthenticatedGet<GroupUser[]>('api/search/students', { email: name });
    }

    findTest(name: string, topic: string): Observable<GroupTest[]> {
        if (true)
            return this.makeAuthenticatedGet<GroupTest[]>('api/search/tests', { name: name, topic: topic });
        else
            return of(null);
    }

    refresh() {
        this.refreshSearch.next(true);
    }
}