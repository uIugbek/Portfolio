import { Component, OnInit } from '@angular/core';

@Component({
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {

  public message: string;

  constructor() { 
    this.message = "This Project developed by Ulugbek Tangmatov!";
  }

  ngOnInit() {
  }

}
