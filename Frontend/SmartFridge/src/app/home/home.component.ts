import { Component, OnInit } from '@angular/core';
import { BackendService } from '../backend.service';
import { Beverage } from '../Beverage';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  beveragesInTheFridge: Beverage[] = [];

  constructor(private backend: BackendService) { }

  ngOnInit(): void 
  {
    this.onGetdrank();
  }

  onGetdrank()
  {
    console.log(this.beveragesInTheFridge[0]);
    this.backend.getBeverages().subscribe(
      (response: Beverage[] ) =>
      {
        console.log(response);
        this.beveragesInTheFridge = response;
      },
      (error) => console.log(error)
    );
  }

}
