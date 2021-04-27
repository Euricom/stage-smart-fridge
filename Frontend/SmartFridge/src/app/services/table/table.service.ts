import { HttpClient,} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ItableAndMinAmount } from './table-and-minamount';

@Injectable({
  providedIn: 'root'
})
export class TableService {

  constructor(private http: HttpClient) { }

  getBeverages()
  {
    const urlLoc = "https://localhost:5001/Table/fridgeContent";
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo";
    return this.http.get<ItableAndMinAmount>(urlLoc);
  }
}
