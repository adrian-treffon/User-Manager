import { Component, OnInit } from '@angular/core';
import { userService } from "../_services/UserService";
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';
import { FormBuilder,Validators, FormGroup  } from '@angular/forms';
import {Router} from "@angular/router";

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  public user : User;
  public editForm : FormGroup;
  public fileUploaded : boolean = false;
  public dataLoaded = false;

  constructor(private userService: userService,private route: ActivatedRoute,private formBuilder: FormBuilder,private router: Router) {

   this.route.paramMap.subscribe(id => {
      this.userService.getUser(id.get('id')).subscribe(data => {
        this.user = data;
        this.editForm = this.formBuilder.group({
          firstName: [data.firstName, Validators.required],
          lastName: [data.lastName, Validators.required],
          profession: [data.profession, Validators.required],
        });
        this.dataLoaded = true;
        });
    })
  }

  ngOnInit(): void {
  }

  public onSubmit(user: User) {
    user.id = this.user.id;
    user.photoUrl = this.user.photoUrl;
    this.userService.updateUser(user).subscribe(() => {
      this.router.navigate(['/users'])
    });
  }

  public onSelectFile(files: File[]) {
    this.userService.uploadProfilePicture(files,this.user.id).subscribe(data => {
      this.user.photoUrl = data;
      this.fileUploaded = true;
    })
  }
}

