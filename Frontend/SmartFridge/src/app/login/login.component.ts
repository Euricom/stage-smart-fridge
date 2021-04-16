import { Component, OnInit} from '@angular/core';
import {Md5} from 'ts-md5/dist/md5';
import {BackendService} from '../backend.service';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  email: string = "";
  password: string = "";
  token: string = "";
  hide: boolean = true;
  constructor(private backendService: BackendService) { }

  form: FormGroup = new FormGroup({
    'emailAdress': new FormControl(null, [Validators.required, Validators.email]),
    'password': new FormControl(null, Validators.required)
  });
  

  ngOnInit(): void {
  }

  getErrorMessageEmail() {
    if (this.form.get('emailAdress')?.hasError('required')) {
      return 'Je moet een e-mailadres geven';
    }
    return "Geef een geldig e-mailadres"
  }

  onSubmit() {  
    this.email = this.form?.get('emailAdress')?.value;
    this.password = this.form?.get('password')?.value;  
    this.token = this.backendService.login(this.email, this.password);
    console.log(this.token);
  }
  

}
