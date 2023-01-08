import { HttpClient, HttpContext, HttpParams } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { AbstractControl, AsyncValidatorFn, FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs";
import { environment } from "../../../../environments/environment";
import { ICity } from "../../../Interfaces/ICity";
import { ICountry } from "../../../Interfaces/ICountry";
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-test-page-one-details',
  templateUrl: './test-page-one-detail.component.html',
  styleUrls: ['./test-page-one-detail.component.css']
})

export class TestPageOneDetailComponent implements OnInit {

  form!: FormGroup;             // ReactiveForm
  city!: ICity;                 // Type Safety / Also Object Literal
  countries!: ICountry[];       // Type Safety / Also Object Literal
  id?: number;                  // State of Page

  constructor(
    private http: HttpClient,                   // Http Methods  
    private activatedRoute: ActivatedRoute,     // URL Parameters  
    private router: Router,                     // Routing to other URL - Re-Routing After Action
    private fb: FormBuilder                     // Create Forms
  ) { }

  ngOnInit(): void {

    this.form = this.fb.group({
      name: ["", Validators.required],
      lat: ["", Validators.required],
      lon: ["", Validators.required],
      countryId: ["", Validators.required]      
    }, { asyncValidators: this.isDupeCity() }
    );                      // Async Method call to Back-End

    this.loadData();
  } 

  public loadData(): void {                     // Difference between Create & Detail happens in loadData()

    this.loadCountries();                       // Load all Countries

    // To get to the Detail page - Click City or Click Add
    // If Click City, (Id) Present - ID present on the URL
    // If Click Add, (Id) Absent - ID absent from the URL
    var idParams = this.activatedRoute.snapshot.paramMap.get("id");

    if (idParams) {                             // If idParam is present, assing it to this.id

      this.id = +idParams;                      

      // Validation Step to Check if Number or Not
      if (isNaN(this.id)) {
        console.log("Parameter can only be a Number");
        return undefined;
      }

      var url = environment.baseUrl + 'api/cities/' + this.id;        // Get the URL to send to HTTP Get

      this.http.get<any>(url).subscribe(
        result => {
          this.city = result.value;             // Assign result to this.city
          this.form.patchValue(this.city);      // Patch the Values in the Form
        }, err => console.error(err));
    }

  }


  // This methods only gets called from the Html File
  public onSubmit(): void {

    if (this.city) {
      var city = this.city;             // This might be Undefined
    }
    else {
      var city = <ICity>{};
    }

    // Form Values to Var
    city.name = this.form.controls['name'].value;
    city.lat = +this.form.controls['lat'].value;                    // + convert to number
    city.lon = +this.form.controls['lon'].value;                    // + convert to number
    city.countryId = +this.form.controls['countryId'].value;

    if (this.city) {                                                  // If city is Defined,

      var url = environment.baseUrl + 'api/cities/' + city.id;        // Get the URL to send to the Http Method

      // PUT Method for Updating
      this.http
        .put<ICity>(url, city)
        .subscribe(() => {
          console.log("City " + city.id + " - " + city.name + " has been updated.");
        }, error => console.error(error));
    }
    else {

      var url = environment.baseUrl + 'api/cities/';

      // POST Method for Creating
      this.http.post<ICity>(url, city).subscribe(results => {
        console.log("City " + results.id + " - " + results.name + " has been created.");
      }, error => console.error(error));
    }

    this.router.navigate(['/testpageones']);
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

