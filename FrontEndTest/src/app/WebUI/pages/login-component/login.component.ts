import { Route } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginRequest } from '../../../model_interfaces/ILoginRequest';
import { LoginResult } from '../../../model_interfaces/ILoginResult';
import { AuthService } from '../../../service/NewFolder/AuthService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  hide = true;
  form!: FormGroup;
  loginResult?: LoginResult;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private fb: FormBuilder) { }

  ngOnInit(): void {

    this.form = this.fb.group({
      Email: ['', Validators.required],
      Password: ['', Validators.required]
    });

  }

  onSubmit() {
    var loginRequest = <LoginRequest>{};
    loginRequest.email = this.form.controls['Email'].value;
    loginRequest.password = this.form.controls['Password'].value;

    this.authService.login(loginRequest).subscribe(results => {
      console.log(results);
      this.loginResult = results;

      if (results.success && results.token) {
        localStorage.setItem(this.authService.tokenKey, results.token);
      }

    }, error => {
      console.log(error);
      if (error.status == 401) { this.loginResult = error.error; }
    });
  }

}
