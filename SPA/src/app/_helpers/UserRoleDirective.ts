import { Directive, Input, OnInit } from "@angular/core";
import { TemplateRef, ViewContainerRef } from "@angular/core";
import { AuthService } from "../_services/AuthService";


@Directive({ selector: '[appUserRole]' })
export class UserRoleDirective{
  constructor(
    private authService: AuthService,
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef
) { }

 
@Input()
    set appUserRole(userRole: string) {
      const allowed = userRole == localStorage.getItem("role");
    
      if (allowed) {
        this.viewContainer.createEmbeddedView(this.templateRef);
      } else {
        this.viewContainer.clear();
      }
    }
}
