import { Component, OnInit } from "@angular/core";
import { userService } from "../services/UserService";
import { User } from "../interfaces/user";


@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {

  public users : User[] = [];
  public tableView: boolean = false;
  public tileView: boolean = true;
  public dataLoaded = false;

  constructor(private userService: userService) {}

  public getUsers() {
    this.userService.getUsers().subscribe(data => {
      this.users=data;
      this.dataLoaded = true;
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

  public handleUserDelete = (id : number) => {
    this.userService.deleteUser(id).subscribe( () => {
      this.getUsers();
   });
  }

}
