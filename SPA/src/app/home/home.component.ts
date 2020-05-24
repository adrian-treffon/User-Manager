import { Component, OnInit, PipeTransform } from "@angular/core";
import { userService } from "../_services/UserService";
import { User } from "../_models/user";
import { FormControl } from '@angular/forms';
import { DecimalPipe } from '@angular/common';
import { startWith, map } from 'rxjs/operators';





@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {

  private _users : User[] = [];
  public tableView: boolean = false;
  public tileView: boolean = true;
  public dataLoaded = false;

  public page = 1;
  public pageSize = 4;
  public collectionSize;
  public filter = new FormControl('');

  public get users(): User[] {
    return this._users
      .map((country, i) => ({id: i + 1, ...country}))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  constructor(private userService: userService) {
  }

  public getUsers() {
    this.userService.getUsers().subscribe(data => {
      this._users=data;
      this.dataLoaded = true;
      this.collectionSize = data.length;
    });
  }

  
  ngOnInit(){
     this.getUsers();
  }

  public showTable = () => {
    this.tileView = false;
    this.tableView = true;
  };

  public showTile = () => {
    this.tableView = false;
    this.tileView = true;
  };

  public handleUserDelete = (id : string) => {
    this.userService.deleteUser(id).subscribe( () => {
      this.getUsers();
   });
  }

}
