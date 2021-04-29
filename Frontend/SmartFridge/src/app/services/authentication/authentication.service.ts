import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginValues, ILoginValues } from './login-values';
import { UserService } from '../users/user.service';
import {DatePipe, formatDate} from '@angular/common';
import { environment } from '../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient, private userService: UserService, private datePipe: DatePipe) { }

  dateString: string = "";
  unformattedDate = new Date();
  testDate = new Date();

  registerNewPerson(email: string, password: string, firstName: string, lastName: string  )
  {
    const url = environment.apiUrl + "/Frigo/register";
    return this.http.post<string>(url,{email, password, firstName, lastName});
  }

  login(email: string, password: string)
  {
    const url = environment.apiUrl + "/Frigo/login";
    return this.http.post<ILoginValues>(url, {email, password})
  }

  saveTokenAndUsername(tok: LoginValues)
  {
    localStorage.setItem('token', tok.token)
    localStorage.setItem('expires_at', tok.expiration.toString());
    localStorage.setItem('Id', tok.id);
    localStorage.setItem('username', tok.userName);
  }

  isLogedIn()
  {
    if (localStorage.getItem('expires_at') == null)
    {
      return false;
    }
    this.unformattedDate = new Date(localStorage.getItem('expires_at') || '{}');
    this.dateString = formatDate(this.unformattedDate, 'yyyy-MM-dd hh:mm:ss', 'en_US')
    const expirationDate = new Date(this.dateString);
    
    this.unformattedDate = new Date();
    this.dateString = formatDate(new Date(), 'yyyy-MM-dd hh:mm:ss', 'en_US')
    const currentDate = new Date(this.dateString);
    
    if(currentDate > expirationDate)
    {
      return false;
    }
    return true;
  }

    


  logout() {
    localStorage.clear();
  }

  getUsername(): string
  {
    return localStorage.getItem('username')|| "Gebruiker";
  }

  
}
