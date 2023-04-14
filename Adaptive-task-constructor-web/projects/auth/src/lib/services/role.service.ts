import { Injectable } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";
import { BehaviorSubject } from "rxjs";
import { AuthenticationService } from "./authentication.service";

@Injectable({providedIn: 'root'})
export class RoleService {
    readonly role = new BehaviorSubject<string>('');
    protected readonly roleField = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';


    constructor(private jwtHelper: JwtHelperService, private authenticationService: AuthenticationService) {
        if(authenticationService.isToken){
            const role = this.getRoleFromToken(authenticationService.token);
            this.role.next(role);
        }
        authenticationService.tokenSubject.subscribe(token => {
            const role = this.getRoleFromToken(token);
            this.role.next(role);
        })
    }
    getRole(){
        const encodedToken = this.authenticationService.token ?? undefined;
        const decodedToken = this.jwtHelper.decodeToken(encodedToken);
        const role = decodedToken[this.roleField];

        return role;
    }

    getRoleFromToken(token: string){
        const encodedToken = token ?? undefined;
        const decodedToken = this.jwtHelper.decodeToken(encodedToken);
        let role: string;
        if(decodedToken){
            role = decodedToken[this.roleField];
        }

        return role;
    }
}