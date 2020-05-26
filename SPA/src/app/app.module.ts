import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { NgbModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import {DecimalPipe} from '@angular/common';

import { AuthInterceptor} from './_helpers/AuthInterceptor';
import { ErrorInterceptor} from './_helpers/ErrorInterceptor';

import { AppComponent } from './app.component';
import { TableComponent } from './table/table.component';
import { TileComponent } from './tile/tile.component';
import { HomeComponent } from './home/home.component';
import { DetailsComponent } from './details/details.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { LoginComponent } from './login/login.component';
import { AuthService } from './_services/AuthService';
import { UserRoleDirective } from './_helpers/UserRoleDirective';
import { NavbarComponent } from './navbar/navbar.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { WelcomeComponent } from './welcome/welcome.component';

@NgModule({
  declarations: [
    AppComponent,
    TableComponent,
    TileComponent,
    HomeComponent,
    DetailsComponent,
    CreateComponent,
    EditComponent,
    LoginComponent,
    UserRoleDirective,
    NavbarComponent,
    WelcomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AuthService,
    DecimalPipe,
    NgbActiveModal
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
