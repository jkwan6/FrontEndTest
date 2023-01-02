import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  showDelay = new FormControl(250);
  hideDelay = new FormControl(0);
  constructor() {

}

  ngOnInit(): void {
  }

}
