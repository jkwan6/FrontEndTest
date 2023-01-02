import { NgModule } from '@angular/core';
import { Routes, RouterModule, Router } from '@angular/router';
import { HomeComponent } from './WebUI/pages/home/home.component';
import { TestPageOneComponent } from './WebUI/pages/test-page-1/test-page-one.component';
import { TestPageTwoComponent } from './WebUI/pages/test-page-2/test-page-two.component';
import { TestPageThreeComponent } from './WebUI/pages/test-page-3/test-page-three.component';
import { TestPageOneDetailComponent } from './WebUI/pages/test-page-1-detail/test-page-one-detail.component';
import { TestPageTwoDetailComponent } from './WebUI/pages/test-page-2-detail/test-page-two-detail.component';
import { TestPageOneCreateComponent } from './WebUI/pages/test-page-1-create/test-page-one-create.component';
import { TestPageFourComponent } from './WebUI/pages/test-page-4/test-page-four.component';
import { TestPageFiveComponent } from './WebUI/pages/test-page-5/test-page-five.component';


const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'testpageone', component: TestPageOneComponent },
  { path: 'testpagetwo', component: TestPageTwoComponent },
  { path: 'testpagethree', component: TestPageThreeComponent },
  { path: 'testpagefour', component: TestPageFourComponent },
  { path: 'testpageone/:id', component: TestPageOneDetailComponent },
  { path: 'testpagetwo/:id', component: TestPageTwoDetailComponent },
  { path: 'testpageonecreate', component: TestPageOneCreateComponent },
  { path: 'testpagetwodetail', component: TestPageTwoDetailComponent },
  { path: 'testpagefive', component: TestPageFiveComponent }
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
