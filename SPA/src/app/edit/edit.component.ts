import { Component, OnInit, Input } from "@angular/core";
import { userService } from "../_services/UserService";
import { User } from "../_models/user";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: "app-edit",
  templateUrl: "./edit.component.html",
  styleUrls: ["./edit.component.css"],
})
export class EditComponent implements OnInit {
  @Input() public user: User;
  public editForm: FormGroup;
  public fileUploaded: boolean = false;
  public dataLoaded = false;

  constructor(
    private userService: userService,
    private formBuilder: FormBuilder,
    public activeModal: NgbActiveModal,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {

    this.editForm = this.formBuilder.group({
             firstName: [this.user.firstName, Validators.required],
             lastName: [this.user.lastName, Validators.required],
             profession: [this.user.profession, Validators.required],
           });

    this.dataLoaded = true;
  }

  public onSubmit(user: User) {
    user.id = this.user.id;
    user.photoUrl = this.user.photoUrl;
    this.userService.updateUser(user).subscribe(() => {
       this.activeModal.close();
       this.toastr.success("Zapisano zmiany")
    },error => {
      this.toastr.error("Błąd podczas edycji użytkownika")
    });
  }

  public onSelectFile(files: File[]) {
    this.userService
      .uploadProfilePicture(files, this.user.id)
      .subscribe((data) => {
        this.user.photoUrl = data.slice(1, data.length - 1).concat(`?timeStamp=${Date.now()}`);
        this.fileUploaded = true;
        this.toastr.success("Zapisz zmiany, aby zatwierdzić nowe zdjęcie")
      });
  }
}
