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


  registerNewPerson(email: string, password: string, FirstName: string, LastName: string  )
  {
    this.personRegisterData.email = email;
    this.personRegisterData.password = password;
    
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/register";
    this.http.post<PersonRegister>(url,this.personRegisterData);
  }

  login(email: string, password: string)
  {
    this.personLoginData.Email = email;
    this.personLoginData.Password = password;

    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/login";
    return this.http.post<TokenValues>(url, this.personLoginData)
    //return this.tokenValues;
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
