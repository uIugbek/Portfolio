import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard.component';

import { UserListComponent } from './user/user-list/user-list.component';
import { AddUpdateUserComponent } from './user/add-update-user/add-update-user.component';

import { PermissionListComponent } from './permission/permission-list/permission-list.component';
import { AddUpdatePermissionComponent } from './permission/add-update-permission/add-update-permission.component';

import { RoleListComponent } from './role/role-list/role-list.component';
import { AddUpdateRoleComponent } from './role/add-update-role/add-update-role.component';

const routes: Routes = [
  { 
    path: 'dashboard', component: DashboardComponent,
    children: [
      { path: 'user', component: UserListComponent },
      { path: 'user/add', component: AddUpdateUserComponent },
      { path: 'user/update/:id', component: AddUpdateUserComponent },

      { path: 'permission', component: PermissionListComponent },
      { path: 'permission/add', component: AddUpdatePermissionComponent },
      { path: 'permission/update/:id', component: AddUpdatePermissionComponent },

      { path: 'role', component: RoleListComponent },
      { path: 'role/add', component: AddUpdateRoleComponent },
      { path: 'role/update/:id', component: AddUpdateRoleComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class DashboardRoutingModule { }
