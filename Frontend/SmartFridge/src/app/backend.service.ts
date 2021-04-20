import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {Beverage} from './Beverage';
import {PersonLogin} from './PersonLogin';
import {PersonRegister} from './PersonRegister';
import { TokenValues } from './TokenValues';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class BackendService {

  constructor(private http: HttpClient) { }
  
  private tokenValues: string = "";
  private personLoginData = new PersonLogin("","");
  private personRegisterData = new PersonRegister("","","","");

  getBeverages(): Observable<Beverage[]>
  {

    const headerDict = 
    {
    }
    
    const requestOptions = {                                                                                                                                                                                 
      headers: new HttpHeaders(headerDict), 
    };
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo";
    return this.http.get<Beverage[]>(url, requestOptions);
  }


  registerNewPerson(email: string, password: string, firstName: string, lastName: string  )
  {
    this.personRegisterData.Email = email;
    this.personRegisterData.Password = password;
    this.personRegisterData.FirstName = firstName;
    this.personRegisterData.LastName = lastName;
    const urlLoc = "https://localhost:5001/Frigo/register";
    
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/register";
    return this.http.post<string>(urlLoc,this.personRegisterData);
  }

  login(email: string, password: string)
  {
    this.personLoginData.Email = email;
    this.personLoginData.Password = password;

    
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/login";
    return this.http.post<TokenValues>(url, this.personLoginData)
  }

  

  saveToken(tok: TokenValues)
  {
    localStorage.setItem('token', tok.token)
    localStorage.setItem('expires_at', tok.expiration.toString());
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
    localStorage.removeItem("token");
    localStorage.removeItem("expires_at");
}
}
