import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication/authentication.service'
import {AbstractControl, FormControl, FormGroup, ValidatorFn, Validators} from '@angular/forms';
import {Router} from '@angular/router'; 
import { Observable, Subscription } from 'rxjs';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit, OnDestroy {

  subscription: Subscription | undefined;
  registerNewPerson$: Observable<string> | undefined;
  hide: boolean = true;
  wrongEmailAdresses: string[] = [];
  
  errorToShow: string = "";
  servererror: boolean = false;
  //This checks that the password contains atleast 1 capital letter, 1 small letter, 1 special karakter, 1 number and in total 8 karakters
  passwordValidator: string = "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$";
 
  constructor(private authenticationService:  AuthenticationService, private route:Router, private _snackBar: MatSnackBar) { }
  
  
  form: FormGroup = new FormGroup({
    'emailAdress': new FormControl(null, [Validators.required, Validators.email, this.forbiddenNameValidator(this.wrongEmailAdresses)]),
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
    if (this.form.get('emailAdress')?.hasError('emailAlreadyInUse')) {
      return 'Dit e-mailadres wordt al gebruikt';
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


  onSubmit() {
    this.registerNewPerson$ = this.authenticationService.registerNewPerson(this.form?.get('emailAdress')?.value, this.form?.get('passwords.password')?.value, this.form?.get('name.FirstName')?.value, this.form?.get('name.LastName')?.value);
    this. subscription = this.registerNewPerson$.subscribe(
      data =>
      {
        this._snackBar.open("U gebruiker is toegevoegd", "sluit", {duration: 3000});
        this.route.navigate(['/login']);
      },
      recievedError =>
      {
        console.log(recievedError)
        this.emailAlreadyInUse();
        //this line is needed because this way the field'll be activated and the error message 'll be shown
        this.form.controls["emailAdress"].setValue(this.form.get('emailAdress')?.value);
      }
    );
  }

   emailAlreadyInUse()
  {
    this.wrongEmailAdresses.push(this.form.get('emailAdress')?.value);
  }


  forbiddenNameValidator(emailList: string[]): ValidatorFn {
    console.log("hier");
    return (control: AbstractControl): {[key: string]: any} | null => {
      for (let i = 0; i < emailList.length; i++)
      {
        if(emailList[i] == this.form.get("emailAdress")?.value)
        {
          return {'emailAlreadyInUse': true}
        }
      }
      return null;
    };
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

  ngOnDestroy()
  {
    this.subscription?.unsubscribe();
  }

}
