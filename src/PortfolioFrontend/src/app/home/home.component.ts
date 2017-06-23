import { Component, OnInit } from '@angular/core';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public message: string;

  constructor() {
      this.message = "Welcome to Portfolio!"
  }

  ngOnInit() {
  }

}
