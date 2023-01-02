import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';


@Injectable({
  providedIn: 'root'    // Singleton bcz Injected in Root
})

export class SideNavService {

  private toggle = new BehaviorSubject<boolean>(false);
    currentToggleStatus = this.toggle.asObservable();

  constructor() { }

    changeToggleStatus(_toggle: boolean) {
      this.toggle.next(_toggle);
    }

}
