import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'home-footer',
  templateUrl: './home-footer.component.html',
  styleUrls: ['./home-footer.component.css']
})
export class HomeFooterComponent implements OnInit {

  public currentYear: number = new Date().getFullYear();

  constructor() { }

  ngOnInit() {
  }

}
