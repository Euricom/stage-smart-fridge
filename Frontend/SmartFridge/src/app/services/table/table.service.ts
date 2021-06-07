import { HttpClient,} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ItableAndMinAmount } from './table-and-minamount';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TableService {

  constructor(private http: HttpClient) { }

  getBeverages()
  {
    const url = environment.apiUrl + "/Table/fridgeContent";
    return this.http.get<ItableAndMinAmount>(url);
  }
}
