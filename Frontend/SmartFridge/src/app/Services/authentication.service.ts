import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { PersonLogin } from '../classes/PersonLogin';
import { PersonRegister } from '../classes/PersonRegister';
import { LoginValues } from '../classes/LoginValues';
import { UserService } from '../Services/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient, private UserService: UserService) { }
  
  
  private personLoginData = new PersonLogin("","");
  private personRegisterData = new PersonRegister("","","","");

  registerNewPerson(email: string, password: string, firstName: string, lastName: string  )
  {
    this.personRegisterData.Email = email;
    this.personRegisterData.Password = password;
    this.personRegisterData.FirstName = firstName;
    this.personRegisterData.LastName = lastName;
    
    
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/register";
    return this.http.post<string>(url,this.personRegisterData);
  }

  login(email: string, password: string)
  {
    this.personLoginData.Email = email;
    this.personLoginData.Password = password;

    const urlLoc = "https://localhost:5001/Frigo/login";
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/login";
    return this.http.post<LoginValues>(urlLoc, this.personLoginData)
  }

  

  saveTokenAndUsername(tok: LoginValues)
  {
    localStorage.setItem('token', tok.token)
    localStorage.setItem('expires_at', tok.expiration.toString());
    localStorage.setItem('Id', tok.id);
    localStorage.setItem('username', tok.userName);
    this.UserService.setUserSettingsAfterLogin(tok.minAmount, tok.emailToSendTo, tok.checkBoxValue, tok.id);
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
