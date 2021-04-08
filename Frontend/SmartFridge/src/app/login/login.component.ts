import { Component, OnInit, ViewChild } from '@angular/core';
import {Md5} from 'ts-md5/dist/md5';
import {BackendService} from '../backend.service'



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email: string = "";
  password: string = "";
  hide: boolean = true;
  constructor(private backendService: BackendService) { }
  

  ngOnInit(): void {
  }

  onSubmit() {
    
    let hash = Md5.hashStr("password");
    const md5 = new Md5();
    console.log(md5.appendStr(this.password).end());
    this.backendService.login(this.email, this.password);
    
 }

}
