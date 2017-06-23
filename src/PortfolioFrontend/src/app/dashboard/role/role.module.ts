import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RoleRoutingModule } from './role-routing.module';
import { RoleListComponent } from './role-list/role-list.component';
import { AddUpdateRoleComponent } from './add-update-role/add-update-role.component';

@NgModule({
  
  imports: [
    FormsModule,
    CommonModule,
    RoleRoutingModule
  ],

  declarations: [
    RoleListComponent,
    AddUpdateRoleComponent
  ],

  exports: [
    RoleListComponent
  ]
})

export class RoleModule { }
