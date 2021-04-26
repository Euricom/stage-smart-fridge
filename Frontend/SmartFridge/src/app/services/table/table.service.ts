import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {Beverage} from './beverage';

@Injectable({
  providedIn: 'root'
})
export class TableService {

  constructor(private http: HttpClient) { }

  

  getBeverages()
  {
    const urlLoc = "https://localhost:5001/Table/fridgeContent";
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo";
    return this.http.get<Beverage[]>(urlLoc);
  }
}
