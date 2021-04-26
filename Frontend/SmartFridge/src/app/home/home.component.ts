import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Beverage } from '../classes/Beverage';
import {Router} from '@angular/router'; 
import { TableService } from '../Services/table.service'
import { AuthenticationService } from '../Services/authentication.service'
import { UserService } from '../Services/user.service';




@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

  


export class HomeComponent implements OnInit {

  
  constructor(private AuthenticationService: AuthenticationService, private TableService: TableService, private route:Router, private UserService: UserService) { }

  displayedColumns: string[] = ['name', 'amount', 'symbol'];
  

  minAmount: number = 10;
  beveragesInTheFridge: Beverage[] = [];
  name: string="";
  panelOpenState = false;

  ngOnInit(): void 
  {
    this.minAmount = this.UserService.getMinAmount();
    this.onGetDrinksFromDatabase();
    this.name = this.AuthenticationService.getUsername();
  }

  onGetDrinksFromDatabase()
  {
    this.TableService.getBeverages().subscribe(
      (response: Beverage[] ) =>
      {
        this.beveragesInTheFridge = response;
      },
      (error) => console.log(error)
    );
  }

  logout()
  {
    this.AuthenticationService.logout();
    this.route.navigate(['/login']);
  }
}
