import { Component, OnInit } from '@angular/core';

import { PermissionService } from '../shared/permission.service';
import { Permission, Permission_Locales } from '../shared/permission.model';

@Component({
  templateUrl: './permission-list.component.html',
  styleUrls: ['./permission-list.component.scss'],
  providers: [PermissionService]
})

export class PermissionListComponent implements OnInit {

  public message: string;
  public permissions: Permission[] = [];
  public permission: Permission = new Permission();

  constructor(private _dataService: PermissionService) {
    this.message = "Permissions";
  }

  ngOnInit() {
    this.getAllPermissions();
  }

  public deletePermission(permission: Permission) {
    this._dataService
      .Delete(permission.id)
      .subscribe(() => {
        this.getAllPermissions();
      }, (error) => {
        console.log(error);
      });
  }

  private getAllPermissions() {
    this._dataService
      .GetAll()
      .subscribe(
      data => this.permissions = data,
      error => console.log(error),
      () => console.log('Get all complete')
      );
  }

}
