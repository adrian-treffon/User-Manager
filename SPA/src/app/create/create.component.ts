import { Component, OnInit } from '@angular/core';
import { FormBuilder,Validators, FormGroup  } from '@angular/forms';
import { User } from '../interfaces/user';
import { userService } from "../services/UserService";
import {Router} from "@angular/router"

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})

export class CreateComponent implements OnInit {

  public createForm : FormGroup;
  constructor(private formBuilder: FormBuilder,private userService: userService,private router: Router) {

    this.createForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      profession: ['', Validators.required],
    });
  }

  ngOnInit(): void {
  }

 public onSubmit(user: User) {
    this.userService.addUser(user).subscribe(() => {
      this.router.navigate(['/users'])
    });
  }

}


