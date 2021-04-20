import { Component, OnInit } from '@angular/core';
import {BackendService} from '../backend.service';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';
import {Router} from '@angular/router'; 

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  
  email: string = "";
  password: string = "";
  firstName: string = "";
  lastName: string = "";
  hide: boolean = true;
 
  constructor(private backendService: BackendService, private route:Router) { }

  form: FormGroup = new FormGroup({
    'emailAdress': new FormControl(null, [Validators.required, Validators.email]),
    'passwords': new FormGroup({
      'password': new FormControl(null, [Validators.required, this.samePasswords.bind(this), Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/)]),
      'passwordRepeat': new FormControl(null, [Validators.required , this.samePasswords.bind(this), Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/)])
    }, { validators: this.test() }),
    'name': new FormGroup({
      'FirstName' : new FormControl(null, [Validators.required, Validators.pattern(/^[A-Za-z]+$/)]),
      'LastName': new FormControl(null, [Validators.required, Validators.pattern(/^[A-Za-z]+$/)])
    })
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
  

  ngOnInit()  {
    
  }

  getErrorMessageEmail() {
    if (this.form.get('emailAdress')?.hasError('required')) {
      return 'Je moet een e-mailadres geven';
    }
    return "Geef een geldig e-mailadres"
  }
  // TODO The error message isn't on point yet, functionality works fine though
  // Try to catch this in 1 blok.
  getErrorMessagePasswordPasword() {
    if (this.form.get('passwords.password')?.hasError('required')) {
      return 'Je moet een wachtwoord geven';
    }
    if (this.form.get('passwords.password')?.hasError('pattern')) {
      return 'Hoofdletter, nummer en spaciaal karakter is verplicht min 8 karakters';
    }
    return "Wachtwoorden komen niet overeen"
  }
  getErrorMessagePasswordPaswordRepead() {
    if (this.form.get('passwords.passwordRepeat')?.hasError('required')) {
      return 'Herhaal het wachtwoord aub';
    }
    if (this.form.get('passwords.passwordRepeat')?.hasError('pattern')) {
      return 'Hoofdletter, nummer en spaciaal karakter is verplicht min 8 karakters';
    }
    return "Wachtwoorden komen niet overeen"
  }
  getErrorMessageFirstName()
  {
    if (this.form.get('name.FirstName')?.hasError('required')) {
      return 'Geef een voornaam aub';
    }
    return "Dit is geen geldige voornaam"
  }

  getErrorMessageLastName()
  {
    if (this.form.get('name.LastName')?.hasError('required')) {
      return 'Geef een achternaam aub';
    }
    return "Dit is geen geldige achternaam"
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
    this.email = this.form?.get('emailAdress')?.value;
    this.password = this.form?.get('passwords.password')?.value;
    this.firstName = this.form?.get('name.FirstName')?.value;
    this.lastName = this.form?.get('name.lastname')?.value;

    this.backendService.registerNewPerson(this.email, this.password, this.firstName, this.lastName).subscribe(
      data =>
      {
        console.log(data);

        this.route.navigate(['/login']);
      },
      recievedError =>
      {
        this.CheckError(recievedError.error.message);
      }
    );
  }

  CheckError(errorMessage: string)
  {
    console.log(errorMessage);
    if(errorMessage == "EmailOrPasswordIsNotCorrect")
    {
      this.wrongPasswordForEmail = true;
    }
  }

}
