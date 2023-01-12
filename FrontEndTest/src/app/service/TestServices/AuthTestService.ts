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

// We need a way to keep track of the login status
    // Can track with the token
    // Need a Toggle Status
    // This whole class is a singleton - can put it in there


export class AuthTestService implements OnInit{

  // Properties
  loginStatus: boolean = false; // Default Status is False
  loginPostUrl?: string;
  form!: FormGroup;

  // Injected Services
  constructor(
    private http: HttpClient,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.loginPostUrl = environment.baseUrl + "api/account/login";

  }

  // authTracking based on existence of Token
  authTrackingInit() {
    var token = localStorage.getItem("key");

    var tokenExists: boolean;
    tokenExists = (token) ? true : false;



  }


  // I want to build the Expression Trees in this service




}
