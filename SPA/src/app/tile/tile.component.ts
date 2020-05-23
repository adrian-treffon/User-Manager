import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { User } from "../interfaces/user";

@Component({
  selector: "app-tile",
  templateUrl: "./tile.component.html",
  styleUrls: ["./tile.component.css"],
})
export class TileComponent implements OnInit {
  @Input() public user: User;

  constructor() {}
  ngOnInit(): void {}

  @Output() public deleteUser: EventEmitter<number> = new EventEmitter<number>();
  public onClick = (id : number) =>
  {
    event.stopPropagation();
    return this.deleteUser.emit(id);
  }

}
