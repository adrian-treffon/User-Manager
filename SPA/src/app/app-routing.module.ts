import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DetailsComponent } from './details/details.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { LoginComponent } from './login/login.component';
import { AuthService } from './_services/AuthService';


const routes: Routes = [
  {path: '',   redirectTo: 'users', pathMatch: 'full' },
  {path : 'users', component : HomeComponent, pathMatch: 'full', canActivate : [AuthService] },
  {path : 'users/:id', component : DetailsComponent, pathMatch: 'full', canActivate : [AuthService] },
  {path : 'create', component : CreateComponent, pathMatch: 'full', canActivate : [AuthService] },
  {path : 'edit/:id', component : EditComponent, pathMatch: 'full', canActivate : [AuthService] },
  {path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
