import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { TestPageFourComponent } from './WebUI/pages/test-page-4/test-page-four.component';
import { TestPageFiveComponent } from './WebUI/pages/test-page-5/test-page-five.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { ConnectionServiceModule, ConnectionServiceOptions, ConnectionServiceOptionsToken } from 'angular-connection-service';
import { LoginComponent } from './WebUI/pages/login-component/login.component';
import { AuthInterceptor } from './service/Interceptors/AuthInterceptor';
import { TestPageSixComponent } from './WebUI/pages/test-page-6/test-page-six.component';


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
    TestPageFourComponent,
    TestPageFiveComponent,
    LoginComponent,
    TestPageSixComponent,
  ],
  // Import other modules so that the app can work with other modules
  imports: [
    ConnectionServiceModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    ReactiveFormsModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production,
      // Register the ServiceWorker as soon as the app is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: 'registerWhenStable:30000'
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi:true
    },

    {
      provide: ConnectionServiceOptionsToken,
      useValue: <ConnectionServiceOptions>{
        heartbeatUrl: environment.baseUrl + 'api/heartbeat'
      }
    }],

  // bootstraps the startup component - AppComponent is the Startup Component in this case
  bootstrap: [AppComponent]
})
export class AppModule { }
