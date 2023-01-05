import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { LoginRequest } from "../../model_interfaces/ILoginRequest";
import { LoginResult } from "../../model_interfaces/ILoginResult";


@Injectable({
  providedIn: 'root',})


export class AuthService {  constructor(
    protected http: HttpClient) {  }

  public tokenKey: string = "token";

  isAuthenticated(): boolean {    return this.getToken() !== null;
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);  }

  login(item: LoginRequest): Observable<LoginResult> {    var url = environment.baseUrl + "api/Account/Login";    return this.http.post<LoginResult>(url, item);
  }}