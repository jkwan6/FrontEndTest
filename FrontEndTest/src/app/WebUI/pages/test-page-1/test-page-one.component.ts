import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { environment } from '../../../../environments/environment';
import { ICity } from '../../../model_interfaces/ICity';
import { MatSort } from '@angular/material/sort';
import { CityService } from '../../../service/CityService/CityService';
import { IApiResult } from '../../../service/IApiResult';

enum Strings {
  id = "id",
  name = "name",
  lat = "lat",
  lon = "lon",
  countryName = "countryName",
  delete = "delete",
  asc = "asc",
  desc = "desc",
}


// Component Decorator are like Attributes
@Component({                                              // Component Decorator defines the class as a component
  selector: 'app-test-page-one',                          // Html Tag
  templateUrl: './test-page-one.component.html',          // HTML Stuff - You could push your HTML to this file
  styleUrls: ['./test-page-one.component.css']            // CSS
})

export class TestPageOneComponent implements OnInit {

  // Angular Table Columns to Display
  public displayedColumns: string[] = [
    Strings.id,
    Strings.name,
    Strings.lat,
    Strings.lon,
    Strings.countryName,
    Strings.delete
  ];

  Cities!: MatTableDataSource<ICity>;  // Generic Class from AngMat Table
  defaultPageIndex: number = 0;
  defaultPageSize: number = 10;
  defaultSortColumn: string = Strings.name;
  defaultSortOrder: Strings.asc | Strings.desc = Strings.asc;
  defaultFilterColumn: string = Strings.name;
  filterQuery?: string;

  @ViewChild(MatPaginator) paginator!: MatPaginator;              // ViewChild Properties
  @ViewChild(MatSort) sort!: MatSort;                             // ViewChild Properties

  constructor(private cityService: CityService) { } // Constructor. DI

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

  // Method - Called by loadData AND Html Page
  getData(event: PageEvent) {

    var sortColumn = (this.sort)
      ? this.sort.active : this.defaultSortColumn;      // Active is the Current string
    var sortOrder = (this.sort)                         
      ? this.sort.direction : this.defaultSortOrder;    // Direction is Ascending/Descending
    var filterQuery = (this.filterQuery)
      ? this.filterQuery : null;
    var filterColumn = (this.filterQuery)
      ? this.defaultFilterColumn : null;


    var observable = this.cityService.getData(
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
      this.Cities = new MatTableDataSource<ICity>(result.data);
    }, error => console.error(error));

  }

  deleteData(id: number) {
    var obs = this.cityService.delete(id);
    console.log(id);
    obs.subscribe(x => console.log(x));
  }


}

