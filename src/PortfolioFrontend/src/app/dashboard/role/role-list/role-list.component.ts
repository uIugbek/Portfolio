import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

import { Role, Role_Locales, RoleInPermission } from '../shared/role.model';
import { RoleService } from '../shared/role.service';
import { PermissionService } from '../../permission/shared/permission.service';
import { Configuration } from '../../../app.constants';

@Component({
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss'],
  providers: [
    RoleService,
    PermissionService
  ]
})
export class RoleListComponent implements OnInit {

  public message: string;
  public roles: Role[] = [];
  public role: Role;

  constructor(
    private dataService: RoleService,
    private http: Http,
    private configuration: Configuration,
    private permissionService: PermissionService
  ) {
    this.message = "Roles";
    this.role = new Role(this.permissionService);
  }

  ngOnInit() {
    this.getAllRoles();
  }

  public deleteRole(role: Role) {
    this.dataService
      .Delete(role.id)
      .subscribe(() => {
        this.getAllRoles();
      }, (error) => {
        console.log(error);
      });
  }

  private getAllRoles() {
    this.dataService
      .GetAll()
      .subscribe(
      data => this.roles = data,
      error => console.log(error),
      () => console.log('Get all complete')
      );
  }
}
