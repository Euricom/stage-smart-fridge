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
  

  private personData = new Person("","");

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


  registerNewPerson(email: string, password: string  )
  {
    this.personData.email = email;
    this.personData.password = password;
    
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/login";
    this.http.post<Person>(url,this.personData);
  }
}
