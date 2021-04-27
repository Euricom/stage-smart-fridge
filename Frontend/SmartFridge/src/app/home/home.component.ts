import { Component, OnInit } from '@angular/core';
import { Beverage } from '../services/table/beverage';
import {Router} from '@angular/router'; 
import { TableService } from '../services/table/table.service'
import { AuthenticationService } from '../services/authentication/authentication.service'
import { UserService } from '../services/users/user.service';
import { tableAndMinAmount } from '../services/table/table-and-minamount';




@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

  


export class HomeComponent implements OnInit {

  
  constructor(private authenticationService: AuthenticationService, private tableService: TableService, private route:Router, private userService: UserService) { }

  displayedColumns: string[] = ['name', 'amount', 'symbol'];

  minAmount: number = 10;
  beveragesInTheFridge: Beverage[] = [];
  name: string="";
  panelOpenState = false;

  ngOnInit(): void 
  {
    this.onGetDrinksFromDatabase();
    this.name = this.authenticationService.getUsername();
  }

  onGetDrinksFromDatabase()
  {
    this.tableService.getBeverages().subscribe(
      response =>
      {
        this.beveragesInTheFridge = response.tableData;
        this.minAmount = response.minAmount;
      },
      (error) => console.log(error)
    );
  }

  logout()
  {
    this.authenticationService.logout();
    this.route.navigate(['/login']);
  }
}
