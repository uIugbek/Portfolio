import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

import { DashboardNavigationComponent } from './dashboard-navigation/dashboard-navigation.component';

@NgModule({

  imports: [
    CommonModule,
    RouterModule
  ],

  declarations: [
    DashboardNavigationComponent
  ],

  exports: [
    DashboardNavigationComponent,
    RouterModule
  ]
})
export class SharedModule { }
