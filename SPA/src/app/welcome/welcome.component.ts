import { Component } from '@angular/core';
import { LoginComponent } from '../login/login.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent  {

  constructor( private modalService: NgbModal) { }

  public login() {
    this.modalService.open(LoginComponent, { centered: true });
  }

}
