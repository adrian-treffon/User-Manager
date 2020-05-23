import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

import { throwError, Observable } from "rxjs";
import { catchError } from "rxjs/operators";
import { User } from "../_models/user";

@Injectable({
  providedIn: "root",
})
export class userService {
  private REST_API_SERVER = "http://localhost:5000/api/users";

  headers = new HttpHeaders()
    .set("Content-Type", "application/json")
    .set("Accept", "application/json");
  httpOptions = { headers: this.headers };

  constructor(private http: HttpClient) {}

  private handleError(error: any) {
    console.error(error);
    return throwError(error);
  }

  public getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.REST_API_SERVER).pipe(catchError(this.handleError));}

  public getUser(id: string): Observable<User> {
    const url = `${this.REST_API_SERVER}/${id}`;
    return this.http.get<User>(url).pipe(catchError(this.handleError));
  }

  public addUser(user: User) {
    return this.http.post<User>(this.REST_API_SERVER, user, this.httpOptions).pipe(catchError(this.handleError));}

  public updateUser(user: User) {
    const url = `${this.REST_API_SERVER}/${user.id}`;
    return this.http.put<User>(url, user, this.httpOptions).pipe(catchError(this.handleError));}

  public deleteUser(id: string) {
    const url = `${this.REST_API_SERVER}/${id}`;
    return this.http.delete(url).pipe(catchError(this.handleError));}

  public uploadProfilePicture(files: File[], id: string) {
    const url = `${this.REST_API_SERVER}/photo/${id}`;
    if (files.length === 0) return;
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append("file", fileToUpload, fileToUpload.name);
    return this.http.post(url, formData, { responseType: "text",}).pipe(catchError(this.handleError));}

    
}
