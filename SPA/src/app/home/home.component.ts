import { Component, OnInit } from "@angular/core";
import { userService } from "../_services/UserService";
import { User } from "../_models/user";
import { SortColumn, SortDirection } from "../_models/sorting";

const compare = (v1: string | number, v2: string | number) =>
  v1 < v2 ? -1 : v1 > v2 ? 1 : 0;

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {
  private _users: User[] = [];

  public tableView = false;
  public tileView = true;
  public dataLoaded = false;

  public page: number;
  public pageSize: number;
  public collectionSize: number;

  public column: SortColumn;
  public direction: SortDirection;
  public filterText: string;

  public get users(): User[] {
    this.saveConfig();
    this.collectionSize = 0;
    return this._users
      .filter((user) => {
        const term = this.filterText.toLocaleLowerCase();
        return (
          user.firstName.toLowerCase().includes(term) ||
          user.lastName.toLowerCase().includes(term) ||
          user.profession.toLowerCase().includes(term) ||
          user.userName.toLowerCase().includes(term) ||
          user.id.toString().includes(term)
        );
      })
      .sort((a, b) => {
        this.collectionSize++;
        let res: number;
        if (typeof a[this.column] == "number") {
          res = compare(parseFloat(a[this.column]), parseFloat(b[this.column]));
        } else res = compare(`${a[this.column]}`, `${b[this.column]}`);
        return this.direction === "asc" ? res : -res;
      })
      .slice(
        (this.page - 1) * this.pageSize,
        (this.page - 1) * this.pageSize + this.pageSize
      );
  }

  constructor(private userService: userService) {}

  public getUsers() {
    this.userService.getUsers().subscribe((data) => {
      this._users = data;
      this.collectionSize = data.length;
      this.getConfig();
      this.dataLoaded = true;
    });
  }

  ngOnInit() {
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

  public handleUserDelete = (id: string) => {
    this.userService.deleteUser(id).subscribe(() => {
      this.getUsers();
    });
  };

  private saveConfig() {
    localStorage.setItem("pageSize", this.pageSize.toString());
    localStorage.setItem("page", this.page.toString());
    localStorage.setItem("column", this.column);
    localStorage.setItem("direction", this.direction);
    localStorage.setItem("filterText", this.filterText);
  }

  public clearConfig() {
    localStorage.removeItem("column");
    localStorage.removeItem("direction");
    localStorage.removeItem("filterText");
    localStorage.removeItem("pageSize");
    localStorage.removeItem("page");
    this.getConfig();
  }

  private getConfig() {
    const col = localStorage.getItem("column");
    const dir = localStorage.getItem("direction");
    const text = localStorage.getItem("filterText");
    const size = localStorage.getItem("pageSize");
    const p = localStorage.getItem("page");

    p ? (this.page = parseInt(p)) : (this.page = 1);
    col ? (this.column = col as SortColumn) : (this.column = "id");
    dir ? (this.direction = dir as SortDirection) : (this.direction = "asc");
    text ? (this.filterText = text) : (this.filterText = "");
    size ? (this.pageSize = parseInt(size)) : (this.pageSize = 4);
  
  }

  public exportToCSV() {
    this.userService.exportToCSV(
      this._users
        .filter((user) => {
          const term = this.filterText.toLocaleLowerCase();
          return (
            user.firstName.toLowerCase().includes(term) ||
            user.lastName.toLowerCase().includes(term) ||
            user.profession.toLowerCase().includes(term) ||
            user.userName.toLowerCase().includes(term) ||
            user.id.toString().includes(term)
          );
        })
        .sort((a, b) => {
          let res: number;
          if (typeof a[this.column] == "number") {
            res = compare(
              parseFloat(a[this.column]),
              parseFloat(b[this.column])
            );
          } else res = compare(`${a[this.column]}`, `${b[this.column]}`);
          return this.direction === "asc" ? res : -res;
        })
    );
  }
}
