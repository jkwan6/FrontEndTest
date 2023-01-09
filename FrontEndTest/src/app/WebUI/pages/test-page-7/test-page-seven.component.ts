import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-tes-page-seven',
  templateUrl: './test-page-seven.component.html',
  styleUrls: ['./test-page-seven.component.css']
})
export class TestPageSevenComponent implements OnInit {

  url?: string;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {





  }

}
