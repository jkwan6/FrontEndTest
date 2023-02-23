import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { SideNavService } from '../../service/DataSharingService/SideNavService';

@Component({
  selector: 'top-app-nav-bar',
  templateUrl: './top-nav-bar.component.html',
  styleUrls: ['./top-nav-bar.component.scss']
})
export class TopNavBarComponent implements OnInit {

  vm = this;
  isPressed: boolean = false;

  showDelay = new FormControl(750);
  hideDelay = new FormControl(0);

  toggleStatus!: boolean;
  subscription!: Subscription;
  constructor(private sideNavService: SideNavService) { }

  ngOnInit(): void {
    this.subscription = this.sideNavService.currentToggleStatus$.subscribe(toggleStatus => this.toggleStatus = toggleStatus)
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  onToggle() {
    if (this.toggleStatus) {
      this.sideNavService.changeToggleStatus(false)
      this.toggleStatus = false;
    }
    else {
      this.sideNavService.changeToggleStatus(true)
      this.toggleStatus = true;
    }
  }

}
