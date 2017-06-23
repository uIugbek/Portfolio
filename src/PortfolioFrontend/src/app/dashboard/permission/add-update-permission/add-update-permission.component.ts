import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Params } from '@angular/router';

import { Permission, Permission_Locales } from '../shared/permission.model';
import { PermissionService } from '../shared/permission.service';
import { Culture } from '../../../shared/models/culture.model';
import { Configuration } from '../../../app.constants';

@Component({
  templateUrl: './add-update-permission.component.html',
  styleUrls: ['./add-update-permission.component.scss'],
  providers: [PermissionService]
})
export class AddUpdatePermissionComponent implements OnInit {

  public id: any;
  public isNew: boolean;
  public message: string;
  public fullPath: string;
  public permission: Permission = new Permission();
  public cultures: Culture[];
  public permissionCodeList: any;

  constructor(
    private dataService: PermissionService,
    private location: Location,
    private configuration: Configuration,
    private route: ActivatedRoute
  ) {
    this.route.params.subscribe(params => {
      this.id = +params['id'];
    });
    this.isNew = !this.id;
    this.message = this.isNew ? 'Add Permission' : 'Update Permission';
    this.cultures = configuration.cultures;
    this.fullPath = this.configuration.host + this.location.path();
  }

  ngOnInit() {
    this.getPermissionCodeList();
    if (!this.isNew) {
      this.route.params
        .switchMap((params: Params) => this.dataService.GetSingle(+params['id']))
        .subscribe((permission: Permission) => this.permission = permission);
    }
  }

  public addOrUpdate() {
    if (this.isNew) {
      this.addPermission();
    }
    else {
      this.updatePermission();
    }
  }

  public addPermission() {
    console.log(this.permission);
    this.dataService
      .Add(this.permission)
      .subscribe(() => {
        this.goBack();
      }, (error) => {
        console.log(error);
      });
  }

  public updatePermission() {
    this.dataService
      .Update(this.permission.id, this.permission)
      .subscribe(() => {
        this.goBack();
      }, (error) => {
        console.log(error);
      });
  }

  private getPermissionCodeList() {
    this.dataService
      .GetPermissionCodeList()
      .subscribe(
      data => this.permissionCodeList = data,
      error => console.log(error),
      () => console.log('Get all complete')
      );
  }

  goBack(): void {
    this.location.back();
  }

}
