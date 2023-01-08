import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ICountry } from '../../../Interfaces/ICountry';
import { CountryService } from '../../../service/CountryService/CountryService';

@Component({
  selector: 'app-test-page-two',
  templateUrl: './test-page-two.component.html',
  styleUrls: ['./test-page-two.component.css']
})
export class TestPageTwoComponent implements OnInit {

  public displayedColumns: string[] = ["id", "name", "iso2", "iso3", "citiesCount"];
  public Countries!: MatTableDataSource<ICountry>;  // Generic Class from AngMat Table
  defaultPageIndex: number = 0;
  defaultPageSize: number = 10;
  public defaultSortColumn: string = "name";
  public defaultSortOrder: "asc" | "desc" = "asc";
  defaultFilterColumn: string = "name";
  filterQuery?: string;

  // Kinda like properties
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private countryService: CountryService) { }

  ngOnInit(): void {
    this.loadData();
  }


  loadData(query?: string) {
    var pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
    this.filterQuery = query;
    this.getData(pageEvent);
  }

  // Method - Called by ngOnInit AND Html Page
  getData(event: PageEvent) {
    var sortColumn = (this.sort)
      ? this.sort.active : this.defaultSortColumn;      // Active is the Current string
    var sortOrder = (this.sort)
      ? this.sort.direction : this.defaultSortOrder;    // Direction is Ascending/Descending
    var filterQuery = (this.filterQuery)
      ? this.filterQuery : null;
    var filterColumn = (this.filterQuery)
      ? this.defaultFilterColumn : null;

    var observable = this.countryService.getData(
      event.pageIndex,
      event.pageSize,
      sortColumn,
      sortOrder,
      filterColumn,
      filterQuery
    );

    observable.subscribe(result => {
      this.paginator.length = result.count;
      this.paginator.pageIndex = result.pageIndex;
      this.paginator.pageSize = result.pageSize;
      this.Countries = new MatTableDataSource<ICountry>(result.data);
    }, error => console.error(error));
  }
}
