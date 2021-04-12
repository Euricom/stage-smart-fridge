import { Component, OnInit } from '@angular/core';
import {Md5} from 'ts-md5/dist/md5';
import {BackendService} from '../backend.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  
 
  hide: boolean = true;
 
  constructor(private backendService: BackendService) { }
  form: FormGroup = new FormGroup({
    'emailAdress': new FormControl(null, [Validators.required, Validators.email]),
    'password': new FormControl(null, [Validators.required]),
    'passwordRepeat': new FormControl(null, [Validators.required])
  });

  emailAdress1 = new FormControl('', [Validators.required, Validators.email]);

  ngOnInit()  {
    
  }

  getErrorMessage() {
    if (this.emailAdress1.hasError('required')) {
      return 'You must enter a value';
    }

    return this.emailAdress1.hasError('email') ? 'Not a valid email' : '';
    return "test"
  }


  onSubmit() {    
    console.log(this.form.get('emailAdress')?.value);
//     this.backendService.registerNewPerson(this.email.value, this.password.value);
//     console.log("wachtwoord : ",  this.password.value);
//     console.log("herhaal :", this.passwordRepeat.value);
  }

}
