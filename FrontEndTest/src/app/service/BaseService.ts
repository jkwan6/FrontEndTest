import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { IApiResult } from './IApiResult';

// Generic Abstract Class
export abstract class BaseService<T> {

  // DI Injection
  constructor(protected client: HttpClient) { }

  abstract getData(
    pageIndex: number,              // Parameters
    pageSize: number,               // Parameters
    sortColumn: string,             // Parameters
    sortOrder: string,              // Parameters
    filterColumn: string | null,    // Parameters
    filterQuery: string | null,     // Parameters
  ): Observable<IApiResult<T>>;     // RETURN THIS TYPE

  abstract get(id: number): Observable<T>;
  abstract put(item: T): Observable<T>;
  abstract post(item: T): Observable<T>;

  protected getUrl(url: string) {
    return environment.baseUrl + url;
  }

}
