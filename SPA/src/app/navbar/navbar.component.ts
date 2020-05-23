import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/AuthService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent  {

  constructor(private authService : AuthService,private router: Router) { }

  get isAuthorized() {
    return this.authService.isLoggedIn();
  }

  public logout() {
      this.authService.logout();
  }


 

}
