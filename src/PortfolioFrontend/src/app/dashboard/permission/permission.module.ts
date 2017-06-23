import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { PermissionRoutingModule } from './permission-routing.module';
import { PermissionListComponent } from './permission-list/permission-list.component';
import { AddUpdatePermissionComponent } from './add-update-permission/add-update-permission.component';

@NgModule({

  imports: [
    FormsModule,
    CommonModule,
    PermissionRoutingModule
  ],

  declarations: [
    PermissionListComponent,
    AddUpdatePermissionComponent
  ],

  exports: [
    PermissionListComponent,
    AddUpdatePermissionComponent
  ]

})

export class PermissionModule { }
