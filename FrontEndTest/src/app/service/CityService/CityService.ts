import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseService } from "../BaseService";
import { IApiResult } from './../IApiResult';
import { ICity } from './../../model_interfaces/ICity';

@Injectable({
  providedIn: 'root'    // Singleton bcz Injected in Root
}) // DI Decorator
export class CityService extends BaseService<ICity>{

  constructor(private http: HttpClient) { super(http); }

  getData(
    pageIndex: number,                // Parameter
    pageSize: number,                 // Parameter
    sortColumn: string,               // Parameter
    sortOrder: string,                // Parameter
    filterColumn: string | null,      // Parameter
    filterQuery: string | null        // Parameter
  ): Observable<IApiResult<ICity>>    // RETURN THIS TYPE
  {
    // Http Parameters sent to Back-End for Observable
    var url = this.getUrl('api/cities');
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
    return this.http.get<IApiResult<ICity>>(url, { params });
  }

  get(id: number): Observable<ICity> {
    var url = this.getUrl("api/cities/" + id);
    return this.http.get<ICity>(url);
  }

  put(item: ICity): Observable<ICity> {
    var url = this.getUrl("api/cities/" + item.id)
    return this.http.put<ICity>(url, item);
  }


  post(item: ICity): Observable<ICity> {
    var url = this.getUrl("api/cities");
    return this.http.post<ICity>(url, item);
  }

  delete(id: number): Observable<any> {
    var url = this.getUrl("api/cities/" + id);
    return this.http.delete(url);
  }



}
