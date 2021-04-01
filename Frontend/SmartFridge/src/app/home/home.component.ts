import { Component, OnInit } from '@angular/core';
import { BackendService } from '../backend.service';
import { Beverage } from '../Beverage';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  dr: Beverage[] = [];

  constructor(private backend: BackendService) { }

  ngOnInit(): void 
  {
    this.onGetdrank();
  }

  onGetdrank()
  {
    console.log(this.dr[0]);
    this.backend.getdrinken().subscribe(
      (response: Beverage[] ) =>
      {
        console.log(response);
        this.dr = response;
      },
      (error) => console.log(error),
      () => console.log('klaar')
    );
  }

}
