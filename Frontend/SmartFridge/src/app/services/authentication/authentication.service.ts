import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { LoginValues, ILoginValues } from './login-values';
import { UserService } from '../users/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient, private userService: UserService) { }

  registerNewPerson(email: string, password: string, firstName: string, lastName: string  )
  {
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/register";
    return this.http.post<string>(url,{email, password, firstName, lastName});
  }

  login(email: string, password: string)
  {
    const urlLoc = "https://localhost:5001/Frigo/login";
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/login";
    return this.http.post<ILoginValues>(urlLoc, {email, password})
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
    return moment().isBefore(this.getExpiration());
  }

  getExpiration() {
    const expiration = localStorage.getItem("expires_at");
    return moment(expiration);
  }   


  logout() {
    localStorage.clear();
  }

  getUsername(): string
  {
    return localStorage.getItem('username')|| "Gebruiker";
  }

  
}
