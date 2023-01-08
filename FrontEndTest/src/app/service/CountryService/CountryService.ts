import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "../BaseService";
import { IApiResult } from './../IApiResult';
import { ICountry } from "../../Interfaces/ICountry";

@Injectable({
  providedIn: 'root'    // Singleton bcz Injected in Root
}) // DI Decorator
export class CountryService extends BaseService<ICountry>{

  constructor(private http: HttpClient) { super(http); }

  getData(
    pageIndex: number,                // Parameter
    pageSize: number,                 // Parameter
    sortColumn: string,               // Parameter
    sortOrder: string,                // Parameter
    filterColumn: string | null,      // Parameter
    filterQuery: string | null        // Parameter
  ): Observable<IApiResult<ICountry>>    // RETURN THIS TYPE
  {
    // Http Parameters sent to Back-End for Observable
    var url = this.getUrl('api/countries');
    var params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("sortColumn", sortColumn.toString())
      .set("sortOrder", sortOrder.toString());

    // If Query String Present
    if (filterColumn && filterQuery) {
      params = params
        .set("filterColumn", filterColumn.toString())
        .set("filterQuery", filterQuery.toString());
    }

    // Use the Parameters Above to get an Observable
    return this.http.get<IApiResult<ICountry>>(url, { params });
  }

  get(id: number): Observable<ICountry> {
    var url = this.getUrl("api/countries/" + id);
    return this.http.get<ICountry>(url);
  }

  put(item: ICountry): Observable<ICountry> {
    var url = this.getUrl("api/countries/" + item.id)
    return this.http.put<ICountry>(url, item);
  }


  post(item: ICountry): Observable<ICountry> {
    var url = this.getUrl("api/countries");
    return this.http.post<ICountry>(url, item);
  }





}
