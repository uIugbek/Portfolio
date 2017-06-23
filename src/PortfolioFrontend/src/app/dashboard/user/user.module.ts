import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { UserRoutingModule } from './user-routing.module';
import { UserListComponent } from './user-list/user-list.component';
import { AddUpdateUserComponent } from './add-update-user/add-update-user.component';

@NgModule({

  imports: [
    FormsModule,
    CommonModule,
    UserRoutingModule,
  ],

  declarations: [
    UserListComponent,
    AddUpdateUserComponent
  ],

  exports: [
    UserListComponent,
    AddUpdateUserComponent
  ]
})

export class UserModule { }
