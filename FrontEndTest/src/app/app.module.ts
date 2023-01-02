import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NavBarComponent } from './WebUI/nav-bar-layout/nav-bar.component';
import { HomeComponent } from './WebUI/pages/home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './app-material.module';
import { FooterBarComponent } from './WebUI/footer-layout/footer-bar.component';
import { TestPageOneComponent } from './WebUI/pages/test-page-1/test-page-one.component';
import { TestPageTwoComponent } from './WebUI/pages/test-page-2/test-page-two.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TestPageThreeComponent } from './WebUI/pages/test-page-3/test-page-three.component';
import { EngineComponent } from './service/ThreeService/engine.component';
import { TestPageOneDetailComponent } from './WebUI/pages/test-page-1-detail/test-page-one-detail.component';
import { TestPageTwoDetailComponent } from './WebUI/pages/test-page-2-detail/test-page-two-detail.component';
import { SideBarComponent } from './WebUI/body-layout/side-bar-html/side-bar.component';
import { BodyLayoutComponent } from './WebUI/body-layout/body-layout.component';
import { TestPageOneCreateComponent } from './WebUI/pages/test-page-1-create/test-page-one-create.component';
import { TestPageFourComponent } from './WebUI/pages/test-page-4/test-page-four.component';
import { TestPageFiveComponent } from './WebUI/pages/test-page-5/test-page-five.component';

// AppModule recognizes the different custom html tag selectors
// Help organimze app into cohesive blocks of functionaility
// Provide boundaries within the app
// Provide template resolution environment - I.e. It looks in the Module for things like the html selectors

// Ng Module Decorator
@NgModule({
  // Declare Components fo the compiler can find it
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    FooterBarComponent,
    TestPageOneComponent,
    TestPageTwoComponent,
    TestPageThreeComponent,
    EngineComponent,
    TestPageOneDetailComponent,
    TestPageTwoDetailComponent,
    SideBarComponent,
    BodyLayoutComponent,
    TestPageOneCreateComponent,
    TestPageFourComponent,
    TestPageFiveComponent,
  ],
  // Import other modules so that the app can work with other modules
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    ReactiveFormsModule,
  ],
  providers: [],

  // bootstraps the startup component - AppComponent is the Startup Component in this case
  bootstrap: [AppComponent]
})
export class AppModule { }
