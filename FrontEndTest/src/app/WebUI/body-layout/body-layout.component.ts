import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDrawer } from '@angular/material/sidenav';
import { Subscription } from 'rxjs';
import { SideNavService } from '../../service/DataSharingService/SideNavService';

@Component({
  selector: 'app-body-layout',
  templateUrl: './body-layout.component.html',
  styleUrls: ['./body-layout.component.css']
})
export class BodyLayoutComponent implements AfterViewInit {

  @ViewChild(MatDrawer) matDrawer!: MatDrawer;

  constructor(private sideNavService: SideNavService) { }

  showFiller = false;
  subscription!: Subscription;
  toggleStatus!: boolean;

  ngAfterViewInit(): void {
    this.subscription = this.sideNavService.currentToggleStatus.subscribe(x => this.matDrawer.toggle(x.valueOf()))
  }


}
