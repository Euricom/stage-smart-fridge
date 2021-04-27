import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Settings, ISettings } from './settings';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  
  constructor(private http: HttpClient) {}

  setUserSettingsInServer(minimum: number, email: string, checkBox: boolean)
  {
    // I can't set this line directly in my http I don't know why. Is it wrog to do this this way
    const Id = localStorage.getItem("Id");
    const urlLoc = "https://localhost:5001/Settings/setSettings";
    const url = "https://frigoapieuricom.azurewebsites.net/Settings/setSettings";
    return this.http.post<string>(urlLoc, {"EmailToSendTo" : email,"UserId": Id,"SendAmount": minimum,"WantToRecieveNotification": checkBox})
  }

  

  

  getUserSettings()
  {
    
    const urlLoc = "https://localhost:5001/Settings/getSettings";
    const url = "https://frigoapieuricom.azurewebsites.net/Settings/getSettings";
    const Id = localStorage.getItem("Id");
    return this.http.get<ISettings>(urlLoc);
  }

}
