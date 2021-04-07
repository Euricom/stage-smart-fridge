import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {Beverage} from './Beverage';
import {Settings} from './Settings';

@Injectable({
  providedIn: 'root'
})
export class BackendService {

  constructor(private http: HttpClient) { }

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



  getSettings(): Observable<Settings[]>
  {
    const headerDict = 
    {
    }
    
    const requestOptions = {                                                                                                                                                                                 
      headers: new HttpHeaders(headerDict), 
    };


    const url = "https://frigoapistudents.azurewebsites.net/frigo";
    return this.http.get<Settings[]>(url, requestOptions);
  }

  setSettings()
  {
    const headerDict = {}
    
    const requestOptions = {                                                                                                                                                                                 
      headers: new HttpHeaders(headerDict), 
    };

    const url = "https://frigoapistudents.azurewebsites.net/frigo";
    this.http.get<Settings[]>(url, requestOptions);
  }
}
