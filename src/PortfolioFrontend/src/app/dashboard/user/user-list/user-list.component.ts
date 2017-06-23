import { Component, OnInit } from '@angular/core';

import { UserService } from '../shared/user.service';
import { User } from '../shared/user.model';

@Component({
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
  providers: [ UserService ]
})

export class UserListComponent implements OnInit {

  public message: string;
  public users: User[] = [];
  public user: User = new User();

  constructor(private dataService: UserService) {
      this.message = "Users";
  }

  ngOnInit() {
      this.getAllUsers();
  }

    public deleteUser(user: User) {
      this.dataService
          .Delete(user.id)
          .subscribe(() => {
              this.getAllUsers();
          }, (error) => {
              console.log(error);
          });
    }

    private getAllUsers() {
      this.dataService
          .GetAll()
          .subscribe(
          data => this.users = data,
          error => console.log(error),
          () => console.log('Get all complete')
          );
    }

}
