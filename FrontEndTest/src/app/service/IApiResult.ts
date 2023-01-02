export interface IApiResult<T> {
  data: T[];              // Array of Type<T>
  pageIndex: number;
  pageSize: number;
  count: number;
  totalPages: number;
  sortColumn: string;
  sortOrder: string;
  filterColumn: string;
  filterQuery: string;
}
