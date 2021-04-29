import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication/authentication.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService, private route:Router) { }
  name: string = "";
  ngOnInit(): void {
    this.name = this.authenticationService.getUsername();
  }
  logout()
  {
    this.authenticationService.logout();
    this.route.navigate(['/login']);
  }

}
