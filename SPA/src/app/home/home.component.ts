import { Component, OnInit } from "@angular/core";
import { userService } from "../_services/UserService";
import { User } from "../_models/user";
import { SortColumn, SortDirection } from '../_models/sorting';

const compare = (v1: string | number, v2: string | number) => v1 < v2 ? -1 : v1 > v2 ? 1 : 0;

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {

  private _users : User[] = [];
 
  public tableView = false;
  public tileView = true;
  public dataLoaded = false;

  public page = 1;
  public pageSize = 4;
  public collectionSize;

  public column: SortColumn = 'id';
  public direction: SortDirection = 'asc';

  public get users(): User[] {
    return this._users
      .sort((a, b) => {
        let res : number;
        if(typeof(a[this.column]) == "number")
        {
          res = compare(parseFloat(a[this.column]),parseFloat(b[this.column]));
         
        }else res = compare(`${a[this.column]}`, `${b[this.column]}`);
        
        return this.direction === 'asc' ? res : -res;
      }).slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
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
