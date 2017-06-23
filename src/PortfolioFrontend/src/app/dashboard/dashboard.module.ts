import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { SharedModule  } from './shared/shared.module';
import { UserModule } from './user/user.module';
import { PermissionModule } from './permission/permission.module';
import { RoleModule } from './role/role.module';
import { DashboardComponent } from './dashboard.component';

@NgModule({

  imports: [
    CommonModule,
    SharedModule,
    DashboardRoutingModule,
    UserModule,
    PermissionModule,
    RoleModule
  ],

  declarations: [
    DashboardComponent,
  ],

  exports: [
    DashboardComponent
  ]

})

export class DashboardModule { }
