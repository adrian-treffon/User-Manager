import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/AuthService';
import { ToastrService } from 'ngx-toastr';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthService,private toastr: ToastrService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.statsu == 401) {
                // auto logout if 401 response returned from api
                this.authenticationService.logout();
                this.toastr.error("Twoje sesja wygasła")
            }
            if (err.statsu == 403) {
              // auto logout if 403 response returned from api
              this.authenticationService.logout();
              this.toastr.error("Nie masz uprawnień")
          }

            return throwError(err);
        }))
    }
}