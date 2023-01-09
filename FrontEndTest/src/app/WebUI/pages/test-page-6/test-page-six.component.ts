import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from '../../../../environments/environment';
import { ILoginRequest } from '../../../Interfaces/ILoginRequest';
import { ILoginResult } from '../../../Interfaces/ILoginResult';

@Component({
  selector: 'app-test-page-six',
  templateUrl: './test-page-six.component.html',
  styleUrls: ['./test-page-six.component.css']
})
export class TestPageSixComponent implements OnInit {

  form!: FormGroup;
  hide = true;
  
  constructor(
    private fb: FormBuilder,
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      Email: ["", Validators.required],
      Password: ["", Validators.required]
    })
  }

  onSubmit() {
    console.log("Clicked")

    this.submitFormValues();

  }

  submitFormValues() {
    var user: ILoginRequest;

    user = <ILoginRequest>{};

    var url = environment.baseUrl + "api/account/login";

    user.email = this.form.controls["Email"].value;
    user.password = this.form.controls["Password"].value;

    var loginResult$ = this.http.post<ILoginResult>(url, user);
    loginResult$.subscribe(loginResult => {
      console.log(loginResult.message);
      console.log(loginResult.token);
      console.log(loginResult.success);

      localStorage.setItem("token", loginResult.token!);
    });





  }


}
