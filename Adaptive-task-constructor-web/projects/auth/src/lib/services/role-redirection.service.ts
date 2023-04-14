import { Injectable } from "@angular/core";
import { InjectSetupWrapper } from "@angular/core/testing";
import { Router } from "@angular/router";
import { RoleService } from "./role.service";

@Injectable({ providedIn: "root" })
export class RoleRedirectionService {
    constructor(private roleService: RoleService, private router: Router) {

    }

    redirect() {
        const role = this.roleService.getRole();
        if (role == 'Teacher') {
            this.router.navigate(['/teacher/']);
        }
        else if (role == 'Student') {
            this.router.navigate(['/student/']);
        }
    }
}