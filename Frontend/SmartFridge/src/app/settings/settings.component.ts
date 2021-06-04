import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication/authentication.service';
import { UserService } from '../services/users/user.service';
import { Settings} from '../services/users/settings';
import {MatSnackBar} from '@angular/material/snack-bar';
import { Observable, Subscription } from 'rxjs';
import { tap } from 'rxjs/operators';


@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  

  constructor(private authenticationService: AuthenticationService, private userService: UserService, private _snackBar: MatSnackBar) { }

  subscription: Subscription | undefined;
  settingsObservableget$: Observable<Settings> | undefined;
  settingsObservableset$: Observable<string> | undefined;
  checked: boolean = false;
  Settings = new Settings("","",0,false);
  
  form: FormGroup = new FormGroup({
    'sendAmount': new FormControl(null, [Validators.required, Validators.pattern(/^[0-9]*$/)]),
    'emailToSendTo': new FormControl(null, [Validators.required, Validators.email]),
    'wantToRecieveNotification': new FormControl(null)
  });

  

 
 
  ngOnInit(): void 
  {
    this.settingsObservableget$ = this.userService.getUserSettings().pipe(tap(settingsFromService => this.form.patchValue(settingsFromService)));
    
  }
 
  onSubmit() 
  {
    this.settingsObservableset$ = this.userService.setUserSettingsInServer(this.form.get('sendAmount')?.value, this.form.get('emailToSendTo')?.value, this.form.get('wantToRecieveNotification')?.value);
    this. subscription = this.settingsObservableset$.subscribe(
          (response: string) =>
          {
            this._snackBar.open("De instellingen zijn aangepast", "sluit", {duration: 3000});
          },
          (error) => 
          {
            this._snackBar.open("De instellingen konden niet worden aangepast", "sluit", {duration: 3000});
          }
        );
  }

 
  
  getErrorMessageMinimum()
  {
    if (this.form.get('Minimum')?.hasError('required')) {
      return 'Je moet een mimimum opgeven';
    }
    if (this.form.get('Minimum')?.hasError('pattern')) {
      return 'Je mag enkel een nummer opgeven.';
    }
    return "fout";
  }

  getErrorMessageEmail() {
    if (this.form.get('Email')?.hasError('required')) {
      return 'Je moet een e-mailadres geven';
    }
    return "Geef een geldig e-mailadres"
  }


  ngOnDestroy()
  {
    this.subscription?.unsubscribe();
  }

  
  
}


