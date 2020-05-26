import { Component } from "@angular/core";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { User } from "../_models/user";
import { userService } from "../_services/UserService";
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: "app-create",
  templateUrl: "./create.component.html",
  styleUrls: ["./create.component.css"],
})
export class CreateComponent {
  public createForm: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private userService: userService,
    public activeModal: NgbActiveModal,
    private toastr: ToastrService
  ) {
    this.createForm = this.formBuilder.group({
      userName: ["", Validators.required],
      password: ["", Validators.required],
      firstName: ["", Validators.required],
      lastName: ["", Validators.required],
      profession: ["", Validators.required],
    });
  }

  public onSubmit(user: User) {
    this.userService.addUser(user).subscribe(() => {
      this.activeModal.close();
      window.location.reload();
      this.toastr.success("Utworzono nowego użytkownika")
    },error => {
      this.toastr.error("Błąd podczas tworzenia nowego użytkownika")
    });
  }
}
