import { HttpClient, HttpContext, HttpParams } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { AbstractControl, AsyncValidatorFn, FormControl, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { ICity } from "../../../model_interfaces/ICity";
import { ICountry } from "../../../model_interfaces/ICountry";
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-test-page-one-details',
  templateUrl: './test-page-one-detail.component.html',
  styleUrls: ['./test-page-one-detail.component.css']
})

export class TestPageOneDetailComponent implements OnInit {


  // name lat lon field - Each field needs its own form control
  //                    - They will all be nested inside a form group
  form!: FormGroup;   // The Form Group - Will nest formControls
  city!: ICity;
  countries!: ICountry[];

  constructor(
    private http: HttpClient,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }


  ngOnInit(): void {
    this.form = new FormGroup(
      {
        name: new FormControl("", Validators.required),
        lat: new FormControl("", Validators.required),
        lon: new FormControl("", Validators.required),
        countryId: new FormControl("", Validators.required)
      }, null, this.isDupeCity());
    this.loadData();
  } // End OnInit

  // Method gets called by ngOnInit
  public loadData(): void {

    this.loadCountries();

    var idParams = this.activatedRoute.snapshot.paramMap.get("id");
    var id = idParams ? +idParams : 0;

    var url = environment.baseUrl + 'api/cities/' + id;
    this.http.get<any>(url).subscribe(
      result => {
        this.city = result.value;
        this.form.patchValue(this.city);
      }, err => console.error(err));
  }

  loadCountries() {
    var url = environment.baseUrl + "api/Countries";
    var params = new HttpParams()
      .set("pageSize", "999")
      .set("sortOrder", "asc")
      .set("sortColumn", "name")
      .set("pageIndex", "0");

    this.http.get<any>(url, { params }).subscribe(
      result => { this.countries = result.data; },
      error => console.error(error)
    );
  }


  // This methods only gets called from the Http File
  public onSubmit(): void {

    // Assigning the city property to a var
    var city = this.city;

    if (city) {
      city.name = this.form.controls['name'].value;
      city.lat = +this.form.controls['lat'].value;    // + convert to number
      city.lon = +this.form.controls['lon'].value;    // + convert to number
      city.countryId = +this.form.controls['countryId'].value;

      var url = environment.baseUrl + 'api/cities/' + city.id;

      this.http
        .put<ICity>(url, city)    // PUT METHOD
        .subscribe(result => {
          console.log("City " + city.id + " - " + city.name + " has been updated.");
        }, error => console.error(error)
        );

      this.router.navigate(['/testpageone']);

    }
  }



  isDupeCity(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{ [key: string]: any } | null> => {

      var city = <ICity>{};
      city.id = (this.city.id) ? this.city.id : 0;         // If this.city.id exists, then do stuff
      city.name = this.form.controls['name'].value;
      city.lat = +this.form.controls['lat'].value;
      city.lon = +this.form.controls['lon'].value;
      city.countryId = +this.form.controls['countryId'].value;
      var url = environment.baseUrl + 'api/Cities/IsDupeCity';

      return this.http.post<boolean>(url, city).pipe(map(result => {
        return (result ? { isDupeCity: true } : null);
      }));
    }
  }
}

