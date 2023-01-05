import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { ConnectionService } from 'angular-connection-service';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  hasNetworkConnection: boolean = true;
  hasInternetAccess: boolean = true;

  constructor(private connectionService: ConnectionService) {
    this.connectionService.monitor().subscribe((currentState: any) => {
      this.hasNetworkConnection = currentState.hasNetworkConnection;
      this.hasInternetAccess = currentState.hasInternetAccess;
    });
  }

  public isOnline() {
    return this.hasNetworkConnection && this.hasInternetAccess;
  }


}
