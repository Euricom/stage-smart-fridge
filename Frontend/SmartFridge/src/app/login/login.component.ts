import { Component, OnInit} from '@angular/core';
import { AuthenticationService } from '../services/authentication/authentication.service'
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router'; 




@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  hide: boolean = true;
  wrongPasswordForEmail: boolean = false;
  constructor(private authenticationService: AuthenticationService, private route:Router) { }

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
    this.authenticationService.login(this.form?.get('emailAdress')?.value, this.form?.get('password')?.value).subscribe(
      data =>
      {
        this.wrongPasswordForEmail = false;
        this.authenticationService.saveTokenAndUsername(data);
        this.route.navigate(['/home']);
      },
      recievedError =>
      {
        this.CheckError(recievedError.error.message);
      }
    );

  }

  CheckError(errorMessage: string)
  {
    if(errorMessage == "EmailOrPasswordIsNotCorrect")
    {
      this.wrongPasswordForEmail = true;
    }
  }

  
  

}
