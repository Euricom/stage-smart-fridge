import { Component, OnInit } from '@angular/core';
import {Md5} from 'ts-md5/dist/md5';
import {BackendService} from '../backend.service';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  
  pasRep: string = 'test';
  hide: boolean = true;
 
  constructor(private backendService: BackendService) { }
  form: FormGroup = new FormGroup({
    'emailAdress': new FormControl(null, [Validators.required, Validators.email]),
    'passwords': new FormGroup({
      'password': new FormControl(null, [Validators.required, this.samePasswords.bind(this)]),
      'passwordRepeat': new FormControl(null, [Validators.required , this.samePasswords.bind(this)])
    }, { validators: this.test() })
  });

  test(): ValidatorFn 
  {
    return (currentControl: AbstractControl): { [key: string]: any } |null => 
    {
      if(this.form?.get('password.password')?.value != this.form?.get('password.passwordRepeat')?.value)
    {
      return{ 'passwordsNotMatching': true};
    }
    return null;
    }
  }
  

  emailAdress1 = new FormControl('', [Validators.required, Validators.email]);

  ngOnInit()  {
    
  }

  getErrorMessageEmail() {
    if (this.form.get('emailAdress')?.hasError('required')) {
      return 'Je moet een e-mailadres geven';
    }
    return "Geef een geldig e-mailadres"
  }

  getErrorMessagePasswordPasword() {
    if (this.form.get('password')?.hasError('required')) {
      console.log('Je moet een wachtwoord gevens');
      return 'Je moet een wachtwoord geven';
    }
    console.log('Wachtwoorden komen niet overeen');
    return "Wachtwoorden komen niet overeen"
  }
  getErrorMessagePasswordPaswordRepead() {
    if (this.form.get('passwordRepeat')?.hasError('required')) {
      console.log('Herhaal het wachtwoord aub');
      return 'Herhaal het wachtwoord aub';
    }
    console.log('Wachtwoorden komen niet overeen');
    return "Wachtwoorden komen niet overeen"
  }



  samePasswords(passwordToControl: FormControl): {[s: string]: boolean} | null
  {
    if(this.form?.get('passwords.password')?.value != this.form?.get('passwords.passwordRepeat')?.value)
    {
      return{ 'passwordsNotMatching': true};
    }
    return null;
  }


  onSubmit() {    
    console.log(this.form?.get('passwords.password')?.value);
    console.log(this.form?.get('passwords.passwordRepeat')?.value)
//     this.backendService.registerNewPerson(this.email.value, this.password.value);
//     console.log("wachtwoord : ",  this.password.value);
//     console.log("herhaal :", this.passwordRepeat.value);
  }

}
