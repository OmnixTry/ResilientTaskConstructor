import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpRequestService } from "projects/shared/src/lib/services/http-request.service";
import { BeUrlService } from "projects/shared/src/lib/services/be-url.service";
import { RegistrationUser } from "../entity/registration-user.entity";
import { RegistrationResponse } from "../entity/reistration-response.entity";
import { BehaviorSubject, Observable, Subject, take, tap } from "rxjs";
import { AuthenticationUser } from "../entity/authentication-user.entity";
import { AuthenticationResponse } from "../entity/authentication-response.entity";
import { GroupService } from "projects/service/src/lib/group/service/group.service";

@Injectable({ providedIn: 'root' })
export class AuthenticationService extends HttpRequestService {
    readonly isAuthenticated = new BehaviorSubject<boolean>(false);
    readonly tokenSubject = new BehaviorSubject<string>('');
    readonly userIdSubject = new BehaviorSubject<string>('');
    readonly urlBase = 'api/Accounts/';

    constructor(http: HttpClient, beUrlService: BeUrlService, private groupService: GroupService) {
        super(http, beUrlService);
        this.isAuthenticated.next(this.isToken());
        if (this.isToken()) {
            this.tokenSubject.next(this.token);
        }
    }

    get token() {
        return localStorage.getItem("token");
    }

    registerUser(user: RegistrationUser): Observable<RegistrationResponse> {
        let route = this.createRoute(this.urlBase + 'registration');
        return this.http.post<RegistrationResponse>(route, user)
    }

    getUserId(): Observable<any> {
        return this.makeAuthenticatedGet<any>(this.urlBase + 'current-user-id');
    }

    logIn(user: AuthenticationUser) {
        let route = this.createRoute(this.urlBase + 'login');
        let request = this.http.post<AuthenticationResponse>(route, user);
        return request.pipe(take(1), tap(res => {
            if (!!res.token) {
                localStorage.setItem("token", res.token)
                this.isAuthenticated.next(true);
                this.tokenSubject.next(res.token);

                this.getUserId().subscribe(s => this.userIdSubject.next(s));
            }
        }));
    }

    logOut() {
        localStorage.removeItem("token");
        this.tokenSubject.next(null);
        this.isAuthenticated.next(false);

        this.groupService.selectedGroup.next(null);
    }

    isToken(): boolean {
        return !!localStorage.getItem('token');
    }

    dummyTeacher() {
        let route = this.createRoute('api/dummy/teacher');
        this.http.options('', this.authOptions);
        //return this.http.get<string>(route, { headers: {'Authorization': `Bearer ${authToken}`} });
        return this.http.get<string>(route, this.authOptions);
    }

    dummyStusent() {
        let route = this.createRoute('api/dummy/student');
        this.http.options('', this.authOptions);
        //return this.http.get<string>(route, { headers: {'Authorization': `Bearer ${authToken}`} });
        return this.http.get<string>(route, this.authOptions);
    }
}