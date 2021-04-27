import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {Router} from '@angular/router'; 
import { AuthenticationService } from '../services/authentication/authentication.service';
import { UserService } from '../services/users/user.service';
import { Settings, ISettings } from '../services/users/settings';
import {MatSnackBar} from '@angular/material/snack-bar';


@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  ;

  constructor(private authenticationService: AuthenticationService, private route:Router, private userService: UserService, private _snackBar: MatSnackBar) { }

  name: string="";
  panelOpenState = false;
  checked: boolean = false;
  Settings = new Settings("","",0,false);
  durationInSeconds = 5;
  
  form: FormGroup = new FormGroup({
    'Minimum': new FormControl(null, [Validators.required, Validators.pattern(/^[0-9]*$/)]),
    'Email': new FormControl(null, [Validators.required, Validators.email]),
    'Checkbox': new FormControl(null)
  });
 
  ngOnInit(): void 
  {
    
    this.name = this.authenticationService.getUsername();
    this.userService.getUserSettings().subscribe(
      (response: ISettings) =>
      {
        this.form.patchValue({"Minimum": response.sendAmount});
        this.form.patchValue({"Email": response.emailToSendTo});
        this.form.patchValue({"Checkbox": response.wantToRecieveNotification});
      },
      (error) => console.log(error)
    );
    
    
  }
 
  onSubmit() 
  {
    this.userService.setUserSettingsInServer(this.form.get('Minimum')?.value, this.form.get('Email')?.value, this.form.get('Checkbox')?.value).subscribe(
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
 
  logout()
  {
    this.authenticationService.logout();
    this.route.navigate(['/login']);
  }
}
