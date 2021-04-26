import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Settings } from './settings';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  
  constructor(private http: HttpClient) {}
  
  
  
  private Settings = new Settings("","",0,false);


  setUserSettingsAfterLogin(minimum: number, email: string, checkBox: boolean, id: string)
  {
    
    this.Settings.emailToSendTo = email;
    this.Settings.sendAmount = minimum;
    this.Settings.wantToRecieveNotification = checkBox;
    this.Settings.userId = id;
    localStorage.setItem("Settings", JSON.stringify(this.Settings))
  }

  setUserSettingsInServer(minimum: number, email: string, checkBox: boolean)
  {
    //To get the id
    this.Settings = this.getUserSettings();
    this.Settings.emailToSendTo = email;
    this.Settings.sendAmount= minimum;
    this.Settings.wantToRecieveNotification = checkBox;
    

    localStorage.setItem("Settings", JSON.stringify(this.Settings));
    
    const urlLoc = "https://localhost:5001/Settings/setSettings";
    const url = "https://frigoapieuricom.azurewebsites.net/Settings/setSettings";
    return this.http.post<string>(urlLoc, this.Settings)
  }

 

  getUserSettings()
  {
    this.Settings = JSON.parse(localStorage.getItem('Settings') || "{}");
    return this.Settings;
  }

  getMinAmount()
  {
    this.Settings = JSON.parse(localStorage.getItem('Settings') || "{}");
    return this.Settings.sendAmount;
  }
}
