import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { User } from "../interfaces/user";

@Component({
  selector: "app-table",
  templateUrl: "./table.component.html",
  styleUrls: ["./table.component.css"],
})
export class TableComponent implements OnInit {
  @Input() public users: User[]=[];

  constructor() {}
  ngOnInit(): void {}

  @Output() public deleteUser: EventEmitter<number> = new EventEmitter<number>();
  public onClick = (id: number) => {
    event.stopPropagation();
    return this.deleteUser.emit(id);
  }

  }
