import { Component } from "@angular/core";
import { AuthService } from "../_services/AuthService";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { LoginComponent } from "../login/login.component";
import { CreateComponent } from "../create/create.component";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.css"],
})
export class NavbarComponent {
  constructor(
    private authService: AuthService,
    private modalService: NgbModal
  ) {}

  get isAuthorized() {
    return this.authService.isLoggedIn();
  }

  get isAdmin() {
    return this.authService.isAdmin();
  }

  public logout() {
    this.authService.logout();
  }

  public create() {
    this.modalService.open(CreateComponent, { centered: true });
  }

}
