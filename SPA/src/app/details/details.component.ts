import { Component, OnInit } from "@angular/core";
import { User } from "../_models/user";
import { userService } from "../_services/UserService";
import { ActivatedRoute } from "@angular/router";
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EditComponent } from '../edit/edit.component';

@Component({
  selector: "app-details",
  templateUrl: "./details.component.html",
  styleUrls: ["./details.component.css"],
})
export class DetailsComponent implements OnInit {
  public user: User;
  public dataLoaded = false;

  constructor(
    private userService: userService,
    private route: ActivatedRoute,
    private modalService: NgbModal
  ) {}

  public edit()
  {
    const modalRef = this.modalService.open(EditComponent,{ centered: true })
    modalRef.componentInstance.user = this.user;

    modalRef.result.then(() => {
      this.ngOnInit();
    })
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((id) => {
      this.userService.getUser(id.get("id")).subscribe( data => {
        this.user = data;
        this.dataLoaded = true;
      });
    });
  }
}
