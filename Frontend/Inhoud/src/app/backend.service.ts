import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {Drinken} from './drinken'

@Injectable({
  providedIn: 'root'
})
export class BackendService 
{

  constructor(private http: HttpClient) { }

  // Observable is omdat de get request ff kan duren.
  getdrinken(): Observable<Drinken[]>
  {

    const headerDict = 
    {
    //   'Content-Type': 'application/json',
    //   'Accept': 'application/json',
    //   'Access-Control-Allow-Origin': 'https://frigo20210224093127.azurewebsites.net/Frigo',
   
    }
    
    const requestOptions = {                                                                                                                                                                                 
      headers: new HttpHeaders(headerDict), 
    };




    const url = "https://frigoapistudents.azurewebsites.net/frigo";
    return this.http.get<Drinken[]>(url, requestOptions);
  }

}
