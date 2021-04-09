import { Component, OnInit } from '@angular/core';
import {Md5} from 'ts-md5/dist/md5';
import {BackendService} from '../backend.service'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  email: string = "";
  password: string = "";
  passwordRepeat: string = "";
  hide: boolean = true;
  passwordHash: string = "";

  passwordsNotMatching: boolean = false;
  constructor(private backendService: BackendService) { }
  

  ngOnInit(): void {
  }

  onSubmit() {
    if(this.password != this.passwordRepeat)
    {
      console.log("ok");
      this.passwordsNotMatching = true;
    }

    const md5 = new Md5();
    this.passwordHash = md5.appendStr(this.password).end().toString();
    // This should give the same response but it gives different hashes I don't think it is a problem but I don't understand it.
    console.log(md5.appendStr(this.password).end().toString());
    console.log(this.passwordHash);
    this.backendService.registerNewPerson(this.email, this.passwordHash);
    
 }
}
