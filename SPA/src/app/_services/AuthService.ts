import { Injectable, ɵConsole } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { tap } from "rxjs/operators";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';


@Injectable()
export class AuthService implements CanActivate {
  private REST_API_SERVER = "http://localhost:5000/api/users";

  constructor(private http: HttpClient,private router: Router) {}

  public login(username: string, password: string) {
       return this.http
      .post(`${this.REST_API_SERVER}/login`, { username, password })
      .pipe(tap(
        data => this.setSession(data),
        error => console.log(error)
      ));
  }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    
    if(this.isLoggedIn())
    {
      const allowedRoles = route.data["role"];

      if(allowedRoles && !allowedRoles.includes(localStorage.getItem("role")))
      {
          this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
          return false;
      }

      return true;
    }

    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;

}

  private setSession(authResult) {
    localStorage.setItem("token", authResult.token);
    localStorage.setItem("role", authResult.role);
  }

  public logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("role");
  }

  public isLoggedIn(): boolean {
    return localStorage.getItem("token") ? true : false;
  }

  public isAdmin() : boolean{
    
    const role = localStorage.getItem("role");

    if(role)
    {
      if(role == "Admin")
      return true;
    }

    return false;
   
  }
}
