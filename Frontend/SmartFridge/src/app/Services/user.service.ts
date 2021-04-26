import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Settings } from '../classes/Settings';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  
  constructor(private http: HttpClient) {}
  
  
  
  private Settings = new Settings("","",0,false);


  setUserSettingsAfterLogin(Minimum: number, Email: string, checkBox: boolean, Id: string)
  {
    
    this.Settings.emailToSendTo = Email;
    this.Settings.sendAmount = Minimum;
    this.Settings.wantToRecieveNotification = checkBox;
    this.Settings.userId = Id;
    localStorage.setItem("Settings", JSON.stringify(this.Settings))
  }

  setUserSettingsInServer(Minimum: number, Email: string, checkBox: boolean)
  {
    //To get the id
    this.Settings = this.getUserSettings();
    this.Settings.emailToSendTo = Email;
    this.Settings.sendAmount= Minimum;
    this.Settings.wantToRecieveNotification = checkBox;
    

    localStorage.setItem("Settings", JSON.stringify(this.Settings));
    
    const urlLoc = "https://localhost:5001/Settings/setSettings";
    const url = "https://frigoapieuricom.azurewebsites.net/Settings/setSettings";
    return this.http.post<string>(urlLoc, this.Settings)
  }

  //not needed I think
  // getUserSettingsFromServer()
  // {
  //   const urlLoc = "https://localhost:5001/Settings/getSettings";
  //   const url = "https://frigoapieuricom.azurewebsites.net/Settings/getSettings";
  //   console.log(localStorage.getItem("Id"))
  //   return this.http.post<Settings>(urlLoc, localStorage.getItem("Id"));
  // }

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
