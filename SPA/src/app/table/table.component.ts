import { Component, Input, Output, EventEmitter } from "@angular/core";
import { User } from "../_models/user";

@Component({
  selector: "app-table",
  templateUrl: "./table.component.html",
  styleUrls: ["./table.component.css"],
})
export class TableComponent {
  @Input() public users: User[]=[];
 
  constructor() {
  }
 
  @Output() public deleteUser: EventEmitter<string> = new EventEmitter<string>();
  public onClick = (id: string) => {
    event.stopPropagation();
    return this.deleteUser.emit(id);
  }

}
