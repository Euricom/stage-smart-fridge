import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {Beverage} from './Beverage';
import {PersonLogin} from './PersonLogin';
import {PersonRegister} from './PersonRegister';

@Injectable({
  providedIn: 'root'
})
export class BackendService {

  constructor(private http: HttpClient) { }
  
  private ret: string = "";
  private personLoginData = new PersonLogin("","");
  private personRegisterData = new PersonRegister("","","","");

  getBeverages(): Observable<Beverage[]>
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


  registerNewPerson(email: string, password: string, FirstName: string, LastName: string  )
  {
    this.personRegisterData.email = email;
    this.personRegisterData.password = password;
    
    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/register";
    this.http.post<PersonRegister>(url,this.personRegisterData);
  }

  login(email: string, password: string)
  {
    this.personLoginData.Email = email;
    this.personLoginData.Password = password;
    console.log(this.personLoginData);

    const url = "https://frigoapieuricom.azurewebsites.net/Frigo/login";
    this.http.post<string>(url, this.personLoginData).subscribe(auth =>{
      console.log(auth);
      this.ret = auth;
    });
    return this.ret;
    // const url2 = "https://frigoapieuricom.azurewebsites.net/Frigo";
    // this.http.get<string>(url2).subscribe(auth =>{
    //     console.log(auth);
    //     this.ret = auth;
    //   });
    // return this.ret;
  }
}
