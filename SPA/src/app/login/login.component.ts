import { Component, OnInit } from "@angular/core";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "../_services/AuthService";
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent {
  public loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.loginForm = this.fb.group({
      username: ["", Validators.required],
      password: ["", Validators.required],
    });
  }

  public login(val: any) {
   
    if (val.username && val.password) {
      this.authService.login(val.username, val.password).subscribe(() => {
        this.router.navigateByUrl("/");
        this.toastr.success("Zalogowano pomyślnie")
      },error => {
        this.toastr.error("Błędny login lub hasło")
      });
    }
  }
}
