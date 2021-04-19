import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { BackendService } from '../backend.service';
import { Beverage } from '../Beverage';
import {Router} from '@angular/router'; 

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  beveragesInTheFridge: Beverage[] = [];
  constructor(private backendService: BackendService, private route:Router) { }

  
  ngOnInit(): void 
  {
    this.onGetdrank();
  }

  onGetdrank()
  {
    console.log(this.beveragesInTheFridge[0]);
    this.backendService.getBeverages().subscribe(
      (response: Beverage[] ) =>
      {
        console.log(response);
        this.beveragesInTheFridge = response;
      },
      (error) => console.log(error)
    );
  }

  loguit()
  {
    this.backendService.logout();
    this.route.navigate(['/login']);
  }

}
