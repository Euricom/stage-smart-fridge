import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {Beverage} from './Beverage';
import {Person} from './Person';

@Injectable({
  providedIn: 'root'
})
export class BackendService {

  constructor(private http: HttpClient) { }
  

  private pers = new Person("","");

  getdrinken(): Observable<Beverage[]>
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


  login(email: string, password: string  )
  {
    this.pers.email = email;
    this.pers.password = password;
    
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/login";
    this.http.post<Person>(url,this.pers);
  }
}
