import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  AsyncValidatorFn,
  FormControl,
  FormGroup,
  Validators,
  FormBuilder
} from '@angular/forms'
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { ICity } from '../../../model_interfaces/ICity';
import { ICountry } from '../../../model_interfaces/ICountry';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-test-page-one-create',
  templateUrl: './test-page-one-create.component.html',
  styleUrls: ['./test-page-one-create.component.css']
})
export class TestPageOneCreateComponent implements OnInit {

  // Properties

  form!: FormGroup;
  city!: ICity;
  countries!: ICountry[];

  constructor(
    private http: HttpClient,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {

    // To Be Refactored
    this.city = <ICity>{};
    this.city.id = 0;

    this.form = this.fb.group(
      {
        name: ['', Validators.required],
        lon:  ['', Validators.required],
        lat:  ['', Validators.required],
        countryId: ['', Validators.required]
      }, { asyncValidators: this.isDupeCity() }
    );

    this.loadCountries();
  }

  loadCountries() {
    var url = environment.baseUrl + "api/countries";

    var params = new HttpParams()
      .set("pageSize", "999")
      .set("sortOrder", "asc")
      .set("sortColumn", "name")
      .set("pageIndex", "0");

    this.http.get<any>(url, { params }).subscribe(
      result => { this.countries = result.data },
      error => console.error(error));
  }

  isDupeCity(): AsyncValidatorFn {

    // Returns an Observable
    var asyncValidatorFunction = (control: AbstractControl):Observable<{ [key: string]: any } | null> => {

      var url = environment.baseUrl + 'api/Cities/IsDupeCity';
      var city = <ICity>
        {
          id: (this.city.id) ? this.city.id : 0,
          name: this.form.controls['name'].value,
          lat: +this.form.controls['lat'].value,
          lon: +this.form.controls['lon'].value,
          countryId: +this.form.controls['countryId'].value
        };

      var observer = this.http.post<boolean>(url, city).pipe(map(backEndBool =>
      {
        var validatorResults = (backEndBool ? { isDupeCity: true } : null);
        return validatorResults;
      }));
      return observer;
    }

    return asyncValidatorFunction;
  }

  onSubmit(): void {
    var city = <ICity>{};

    city.name = this.form.controls["name"].value;
    city.lat = +this.form.controls["lat"].value;
    city.lon = +this.form.controls["lon"].value;
    city.countryId = +this.form.controls["countryId"].value;

    var url = environment.baseUrl + 'api/cities';
    this.http
      .post<ICity>(url, city)   // POST METHOD
      .subscribe(results => {
        console.log("City " + results.name + " has been created.");
      }, error => console.error(error));

    this.router.navigate(["/testpageone"]);
  };

}
