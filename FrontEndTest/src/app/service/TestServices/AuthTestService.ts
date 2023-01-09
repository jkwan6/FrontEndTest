import { HttpClient } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { AbstractControl, FormBuilder, FormGroup, Validators } from "@angular/forms";
import { env } from "process";
import { environment } from '../../../environments/environment';

Injectable(
  {
    providedIn: 'root'
  }
)

export class AuthTestService implements OnInit{

  // Properties
  loginPostUrl?: string;
  form!: FormGroup;

  // Injected Services
  constructor(
    private http: HttpClient,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.loginPostUrl = environment.baseUrl + "api/account/login";

  }



  // I want to build the Expression Trees in this service




}
