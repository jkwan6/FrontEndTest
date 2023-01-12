import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject, tap } from "rxjs";
import { environment } from "../../../environments/environment";
import { ILoginRequest } from "../../Interfaces/ILoginRequest";
import { ILoginResult } from "../../Interfaces/ILoginResult";
import { urlEnum } from "../../Enumerators/FilePath";

@Injectable({
  providedIn: 'root',

export class AuthService {
    protected http: HttpClient) {

  public tokenKey: string = "token";

  private _authStatus = new Subject<boolean>();             // Private Subject
  public authStatus$ = this._authStatus.asObservable();     // Expose the Subject as an Observable for Subscribers

  init(): void {
    if (this.isAuthenticated()) {
      this.setAuthStatus(true);
    }
  }

  login(loginRequest: ILoginRequest): Observable<ILoginResult> {
        if (result.success && result.token)
        {
          localStorage.setItem(this.tokenKey, result.token);
          this.setAuthStatus(true);
        };
      }));
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.setAuthStatus(false);
  }

  // Called by
  isAuthenticated(): boolean {
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);

  }