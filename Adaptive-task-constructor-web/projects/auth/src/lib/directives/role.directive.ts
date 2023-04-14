import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { RoleService } from '../services/role.service';

@Directive({
  selector: '[allowForRole]'
})
export class RoleDirective  {
  private currentSub: Subscription;
  private viewCreated: boolean;

  constructor(private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private roleService: RoleService) { }

  @Input() set allowForRole(allowedRole: string){
    this.currentSub?.unsubscribe();
    this.currentSub = this.roleService.role.subscribe(role => {
      if(allowedRole == role && !this.viewCreated) {
        this.viewContainer.createEmbeddedView(this.templateRef);
        this.viewCreated = true;
      }
      else if (allowedRole != role && this.viewCreated){
        this.viewContainer.clear();
        this.viewCreated = false;
      }
    })
  }
}
