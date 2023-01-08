import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject, tap } from "rxjs";
import { environment } from "../../../environments/environment";
import { LoginRequest } from "../../Interfaces/ILoginRequest";
import { LoginResult } from "../../Interfaces/ILoginResult";
import { urlEnum } from "../../Enumerators/FilePath";

@Injectable({
  providedIn: 'root',})

export class AuthService {  constructor(
    protected http: HttpClient) {  }

  public tokenKey: string = "token";

  private _authStatus = new Subject<boolean>();             // Private Subject
  public authStatus$ = this._authStatus.asObservable();     // Expose the Subject as an Observable for Subscribers

  init(): void {
    if (this.isAuthenticated()) {
      this.setAuthStatus(true);
    }
  }

  login(loginRequest: LoginRequest): Observable<LoginResult> {    var url = environment.baseUrl + "api/Account/Login";    // Tap Operator. Gives it a Side Effect || Doesnt modify the results of the Observable    var loginResult$ = this.http.post<LoginResult>(url, loginRequest).pipe(tap(result => {
        if (result.success && result.token)
        {
          localStorage.setItem(this.tokenKey, result.token);
          this.setAuthStatus(true);
        };
      }));    // Return an Expression Tree.    return loginResult$;
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.setAuthStatus(false);
  }

  // Called by
  isAuthenticated(): boolean {    return (this.getToken() !== null);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);  }
  private setAuthStatus(isAuthenticated: boolean): void {    this._authStatus.next(isAuthenticated);
  }}