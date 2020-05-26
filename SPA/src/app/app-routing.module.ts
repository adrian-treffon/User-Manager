import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { DetailsComponent } from "./details/details.component";
import { AuthService } from "./_services/AuthService";
import { WelcomeComponent } from './welcome/welcome.component';


const routes: Routes = [
  { path: "", redirectTo: "users", pathMatch: "full"},
  {
    path: "users",
    component: HomeComponent,
    canActivate: [AuthService],
  },
  {
    path: "users/:id",
    component: DetailsComponent,
    canActivate: [AuthService],
  },
  { path: "welcome", component: WelcomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
