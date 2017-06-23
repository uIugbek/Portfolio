import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
// import { Http } from '@angular/http';
import { ActivatedRoute, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { Permission } from '../../permission/shared/permission.model';
import { Role, Role_Locales, RoleInPermission } from '../shared/role.model';
import { RoleService } from '../shared/role.service';
import { PermissionService } from '../../permission/shared/permission.service';
import { Culture } from '../../../shared/models/culture.model';
import { Configuration } from '../../../app.constants';

@Component({
  templateUrl: './add-update-role.component.html',
  styleUrls: ['./add-update-role.component.scss'],
  providers: [
    RoleService,
    PermissionService
  ]
})

export class AddUpdateRoleComponent implements OnInit {

  public id: any;
  public isNew: boolean;
  public message: string;
  public fullPath: string;
  public role: Role;
  public cultures: Culture[];
  public permissions: Permission[];

  constructor(
    private route: ActivatedRoute,
    private dataService: RoleService,
    private permissionService: PermissionService,
    private location: Location,
    private configuration: Configuration
  ) {
    this.route.params.subscribe(params => {
      this.id = +params['id'];
    });
    this.isNew = !this.id;
    this.message = this.isNew ? 'Add Permission' : 'Update Permission';
    this.cultures = this.configuration.cultures;
    this.fullPath = this.configuration.host + this.location.path();
    if (!this.isNew) {
      console.log("update");
      this.dataService
        .GetSingle(this.id)
        .subscribe((role: Role) => {
          this.role = role
          console.log(this.role);
        });
      // this.getSingle();
    }
    else {
      this.role = new Role(this.permissionService);
      this.role.initialize();
      console.log("create");
    }
  }

  ngOnInit() {
    this.getPermissions();

    
  }

  public getSingle() {

  }

  public addOrUpdate() {
    if (this.isNew) {
      this.addRole();
    }
    else {
      this.updateRole();
    }
  }

  public addRole() {
    this.dataService
      .Add(this.role)
      .subscribe(() => {
        this.goBack();
      }, (error) => {
        console.log(error);
      });
  }

  public updateRole() {
    this.dataService
      .Update(this.role.id, this.role)
      .subscribe(() => {
        this.goBack();
      }, (error) => {
        console.log(error);
      });
  }

  private getPermissions() {
    this.permissionService
      .GetAll()
      .subscribe(
      data => this.permissions = data,
      error => console.log(error),
      () => {
        // for (let permission of this.permissions) {
        //   var roleInPermission = new RoleInPermission();
        //   roleInPermission.initialize(permission.id);
        //   this.role.roleInPermissions.push(roleInPermission);
        // }
      });
  }

  goBack(): void {
    this.location.back();
  }

}
