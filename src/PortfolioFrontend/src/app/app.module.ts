import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';

import { DashboardModule } from './dashboard/dashboard.module';
import { AppRoutingModule } from './app-routing.module';
import { AboutModule } from './about/about.module';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';


import { AppComponent } from './app.component';

@NgModule({

  declarations: [
    AppComponent
  ],

  imports: [
    AppRoutingModule,
    DashboardModule,
    BrowserModule,
    FormsModule,
    AboutModule,
    HttpModule,
    CoreModule,
    HomeModule,
  ],

  providers: [],
  
  bootstrap: [AppComponent],
  
})
export class AppModule { }
