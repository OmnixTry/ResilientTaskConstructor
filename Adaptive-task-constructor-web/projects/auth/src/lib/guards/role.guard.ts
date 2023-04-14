import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';
import { RoleService } from '../services/role.service';

@Injectable({
  providedIn: 'root'
})
export abstract class RoleGuard implements CanActivate {
  protected abstract get Role(): string;
  protected readonly roleField = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

  constructor(private router: Router, private roleService: RoleService) { }
  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const role = this.roleService.getRole();
      
      if(role === this.Role)
        return true;
      else {
        this.router.navigate(['/forbidden'], { queryParams: { returnUrl: state.url }});      
        return false;
      }
      
  }  
}


@Injectable({
  providedIn: 'root'
})
export abstract class TeacherGuard extends RoleGuard {
  protected get Role(): string {
    return "Teacher";
  };

  constructor(router: Router, roleService: RoleService) 
  { 
    super(router, roleService);
  }  
}

@Injectable({
  providedIn: 'root'
})
export abstract class StudentGuard extends RoleGuard {
  protected get Role(): string {
    return "Student";
  };

  constructor(router: Router, roleService: RoleService) 
  { 
    super(router, roleService);
  }  
}