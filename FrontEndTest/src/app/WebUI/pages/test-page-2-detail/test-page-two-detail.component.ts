import { Component, OnInit } from '@angular/core';
import { ICountry } from '../../../model_interfaces/ICountry';
import { FormControl, FormGroup } from "@angular/forms";
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-test-page-two-detail',
  templateUrl: './test-page-two-detail.component.html',
  styleUrls: ['./test-page-two-detail.component.css']
})
export class TestPageTwoDetailComponent implements OnInit {

  country?: ICountry;
  form!: FormGroup;
  id?: number;

  constructor(
    private http: HttpClient,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup(
      {
        name: new FormControl(""),
        iso2: new FormControl(""),
        iso3: new FormControl("")
      })
    this.loadData();
  }

  public loadData(): void {
    var idParams = this.activatedRoute.snapshot.paramMap.get("id");
    this.id = idParams ? +idParams : 0;

    var entityPresent = (this.id) ? true : false;

    if (entityPresent) {
      var url = environment.baseUrl + 'api/countries/' + this.id;

      this.http.get<ICountry>(url).subscribe(result => {
        this.country = result;
        this.form.patchValue(this.country);
      }, err => console.error(err));
    }
  }

  onSubmit(): void {

    var existingEntity = (this.id) ? true : false;

    var country = (existingEntity) ? this.country : <ICountry>{};
    if (country) {
      country.name = this.form.controls['name'].value;
      country.iso2 = this.form.controls['iso2'].value;
      country.iso3 = this.form.controls['iso3'].value;
    }

    if (existingEntity) {
      var url = environment.baseUrl + 'api/countries/' + country!.id;

      this.http
        .put<ICountry>(url, country)
        .subscribe(result => {
          console.log("Country " + country!.id + " - " + country!.name + " has been updated.");
        }, error => console.error(error)
        );
    }
    else {
      var url = environment.baseUrl + 'api/countries/';

      this.http
        .post<ICountry>(url, country)
        .subscribe(result => {
          console.log("Country " + result.id + " - " + result.name + " has been created.");
        }, error => console.error(error));
    }
    this.router.navigate(['/testpagetwo']);
  }
}

