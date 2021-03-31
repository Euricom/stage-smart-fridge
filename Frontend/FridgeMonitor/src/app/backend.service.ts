import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {Drinken} from './drinken'
import {Settings}  from './Settings'

@Injectable({
  providedIn: 'root'
})
export class BackendService {

  constructor(private http: HttpClient) { }

  // Observable is omdat de get request ff kan duren.
  getdrinken(): Observable<Drinken[]>
  {

    const headerDict = 
    {
    }
    
    const requestOptions = {                                                                                                                                                                                 
      headers: new HttpHeaders(headerDict), 
    };




    const url = "https://frigoapistudents.azurewebsites.net/frigo";
    return this.http.get<Drinken[]>(url, requestOptions);
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
