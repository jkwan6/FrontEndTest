import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import {ISpeedStuff } from '../../../Interfaces/ISpeedStuff'

@Component({
  selector: 'app-tes-page-seven',
  templateUrl: './test-page-seven.component.html',
  styleUrls: ['./test-page-seven.component.css']
})
export class TestPageSevenComponent implements OnInit {

  url?: string;
  key?: string;

  Speed!: MatTableDataSource<ISpeedStuff>;


  public displayedColumns: string[] = [
    "href",
    "id",
    "name",
    //"public_name",
    //"organization",
    //"origin",
    //"destination",
    //"enabled",
    //"draft",
    //"length",
    //"min_number_of_lanes",
    //"minimum_tt",
    //"is_freeway",
    //"direction",
    //"coordinates",
    "latest_stats",
    //"latest_source_id_type_stats",
    "trend",
    //"incidents",
    //"link_params",
    //"excluded_source_id_types",
    //"emulated_travel_time",
    //"closed_or_ignored"
  ];


  //@ViewChild(MatPaginator) paginator!: MatPaginator;              // ViewChild Properties
  //@ViewChild(MatSort) sort!: MatSort;                             // ViewChild Properties


  constructor(private http: HttpClient) { }

  ngOnInit(): void {



    this.key = "XXXXXXXXXXXXXXXXXX";
    this.url = "https://data-exchange-api.vicroads.vic.gov.au/bluetooth_data/links?key=" + this.key;

    var params = new HttpParams;
    params.set("key", this.key);

    this.http.get<ISpeedStuff[]>(this.url).subscribe(x => {
      this.Speed = new MatTableDataSource<ISpeedStuff>(x);
    });

  }

}
