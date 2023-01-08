import { NgModule } from '@angular/core';
import { Routes, RouterModule, Router } from '@angular/router';
import { HomeComponent } from './WebUI/pages/home/home.component';
import { TestPageOneComponent } from './WebUI/pages/test-page-1/test-page-one.component';
import { TestPageTwoComponent } from './WebUI/pages/test-page-2/test-page-two.component';
import { TestPageThreeComponent } from './WebUI/pages/test-page-3/test-page-three.component';
import { TestPageOneDetailComponent } from './WebUI/pages/test-page-1-detail/test-page-one-detail.component';
import { TestPageTwoDetailComponent } from './WebUI/pages/test-page-2-detail/test-page-two-detail.component';
import { TestPageFourComponent } from './WebUI/pages/test-page-4/test-page-four.component';
import { TestPageFiveComponent } from './WebUI/pages/test-page-5/test-page-five.component';
import { LoginComponent } from './WebUI/pages/login-component/login.component';
import { TestPageSixComponent } from './WebUI/pages/test-page-6/test-page-six.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'testpageones', component: TestPageOneComponent },
  { path: 'testpagetwos', component: TestPageTwoComponent },
  { path: 'testpagethree', component: TestPageThreeComponent },
  { path: 'testpagefour', component: TestPageFourComponent },
  { path: 'testpagefive', component: TestPageFiveComponent },
  { path: 'testpagesix', component: TestPageSixComponent },

  { path: 'testpageone/:id', component: TestPageOneDetailComponent },
  { path: 'testpageone', component: TestPageOneDetailComponent },

  { path: 'testpagetwo/:id', component: TestPageTwoDetailComponent },
  { path: 'testpagetwodetail', component: TestPageTwoDetailComponent },

  { path: 'login', component: LoginComponent },

]


@NgModule({
  imports: [
    RouterModule.forRoot(routes,
      {
      scrollPositionRestoration: 'enabled'
    })
],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
