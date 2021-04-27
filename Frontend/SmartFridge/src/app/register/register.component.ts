import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication/authentication.service'
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router'; 

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  
 
  hide: boolean = true;
  
  errorToShow: string = "";
  servererror: boolean = false;
  //This checks that the password contains atleast 1 capital letter, 1 small letter, 1 special karakter, 1 number and in total 8 karakters
  passwordValidator: string = "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$";
 
  constructor(private authenticationService:  AuthenticationService, private route:Router) { }

  form: FormGroup = new FormGroup({
    'emailAdress': new FormControl(null, [Validators.required, Validators.email]),
    'passwords': new FormGroup({
      'password': new FormControl(null, [Validators.required, Validators.pattern(this.passwordValidator)]),
      'passwordRepeat': new FormControl(null, [Validators.required, Validators.pattern(this.passwordValidator)])
    }),  
    'name': new FormGroup({
      'FirstName' : new FormControl(null, [Validators.required, Validators.pattern(/^[A-Za-z]+$/)]),
      'LastName': new FormControl(null, [Validators.required, Validators.pattern(/^[A-Za-z]+$/)])
    })
  });


  ngOnInit()  {
    this.servererror = false;
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
    return "Fout met de wachtwoorden"
  }
  getErrorMessagePasswordPaswordRepead() {
    if (this.form.get('passwords.passwordRepeat')?.hasError('required')) {
      return 'Herhaal het wachtwoord aub';
    }
    if (this.form.get('passwords.passwordRepeat')?.hasError('pattern')) {
      return 'Hoofdletter, nummer en spaciaal karakter is verplicht min 8 karakters';
    }
    if(this.form.get('passwords.passwordRepeat')?.hasError('notEqual'))
    {
      return "Wachtwoorden komen niet overeen"
    }
    return "Fout met de wachtwoorden"
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
    this.authenticationService.registerNewPerson(this.form?.get('emailAdress')?.value, this.form?.get('passwords.password')?.value, this.form?.get('name.FirstName')?.value, this.form?.get('name.LastName')?.value).subscribe(
      data =>
      {
        this.route.navigate(['/login']);
      },
      recievedError =>
      {
        this.CheckError(recievedError.error.message);
      }
    );
  }

  CheckError(error : string)
  {
    switch(error) { 
      case "EmailAlreadyExists": { 
         this.errorToShow = "E-mail adres bestaat al";
         this.servererror = true;
         break; 
      } 
      case "WrongPasswordStructure": { 
        //Normaly this error 'll never show itself.
        this.errorToShow = "Hoofdletter, nummer en spaciaal karakter is verplicht min 8 karakters bij het wachtwoord";
        this.servererror = true; 
         break; 
      } 
      case "UserNameAlreadyExists": { 
        this.errorToShow = "Gebruikersnaam ( voornaam + achternaam) bestaat al";
         this.servererror = true; 
        break; 
     } 
      default: { 
        this.errorToShow = "Gbruiker kon niet worden aangemaakt";
        this.servererror = true;
         break; 
      } 
   }
  }

}
