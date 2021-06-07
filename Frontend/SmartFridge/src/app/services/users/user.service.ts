import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ISettings } from './settings';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  
  constructor(private http: HttpClient) {}

  setUserSettingsInServer(minimum: number, email: string, checkBox: boolean)
  {
    // I can't set this line directly in my http I don't know why. Is it wrog to do this this way
    const Id = localStorage.getItem("Id");
    const url = environment.apiUrl + "/Settings/setSettings"
    return this.http.post<string>(url, {"EmailToSendTo" : email,"UserId": Id,"SendAmount": minimum,"WantToRecieveNotification": checkBox})
  }

  getUserSettings()
  {
    const url = environment.apiUrl + "/Settings/getSettings"
    const Id = localStorage.getItem("Id");
    return this.http.get<ISettings>(url);
  }

}
