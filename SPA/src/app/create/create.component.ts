import { Component, OnInit } from '@angular/core';
import { FormBuilder,Validators, FormGroup  } from '@angular/forms';
import { User } from '../_models/user';
import { userService } from "../_services/UserService";
import {Router} from "@angular/router"

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})

export class CreateComponent  {

  public createForm : FormGroup;
  constructor(private formBuilder: FormBuilder,private userService: userService,private router: Router) {

    this.createForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      profession: ['', Validators.required],
    });
  }

 public onSubmit(user: User) {
    this.userService.addUser(user).subscribe(() => {
      this.router.navigate(['/users'])
    });
  }

}


