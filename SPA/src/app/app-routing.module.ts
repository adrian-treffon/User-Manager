import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DetailsComponent } from './details/details.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';


const routes: Routes = [
  {path: '',   redirectTo: 'users', pathMatch: 'full' },
  {path : 'users', component : HomeComponent, pathMatch: 'full' },
  {path : 'users/:id', component : DetailsComponent, pathMatch: 'full' },
  {path : 'create', component : CreateComponent, pathMatch: 'full' },
  {path : 'edit/:id', component : EditComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
