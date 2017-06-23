import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import 'rxjs/add/operator/switchMap';

import { User } from '../shared/user.model';
import { UserService } from '../shared/user.service';

@Component({
  templateUrl: './add-update-user.component.html',
  styleUrls: ['./add-update-user.component.scss'],
  providers: [ UserService ]
})

export class AddUpdateUserComponent implements OnInit {

  public id: any;
  public isNew: boolean;
  public message: string;
  public user: User = new User();

  constructor(
    private route: ActivatedRoute,
    private dataService: UserService,
    private location: Location
  ) {
    this.route.params.subscribe(params => {
      this.id = +params['id'];
    });
    this.isNew = !this.id;
    this.message = this.isNew ? 'Add User' : 'Update User';
  }

  ngOnInit() {
    if (!this.isNew) {
      this.route.params
        .switchMap((params: Params) => this.dataService.GetSingle(+params['id']))
        .subscribe((user: User) => this.user = user);
    }
  }

  public addOrUpdate() {
    if (this.isNew) {
      this.addUser();
    }
    else {
      this.updateUser();
    }
  }

  public updateUser() {
    this.dataService
      .Update(this.user.id, this.user)
      .subscribe(() => {
        this.goBack();
      }, (error) => {
        console.log(error);
      });
  }

  public addUser() {
    this.dataService
      .Add(this.user)
      .subscribe(() => {
        this.goBack();
      }, (error) => {
        console.log(error);
      });
  }

  goBack(): void {
    this.location.back();
  }

}
