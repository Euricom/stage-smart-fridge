import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {Router} from '@angular/router'; 
import { AuthenticationService } from '../Services/authentication.service';
import { UserService } from '../Services/user.service';
import { Settings } from '../classes/Settings';


@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  ;

  constructor(private AuthenticationService: AuthenticationService, private route:Router, private UserService: UserService) { }

  name: string="";
  panelOpenState = false;
  checked: boolean = false;
  Settings = new Settings("","",0,false);

  
  form: FormGroup = new FormGroup({
    'Minimum': new FormControl(null, [Validators.required, Validators.pattern(/^[0-9]*$/)]),
    'Email': new FormControl(null, [Validators.required, Validators.email]),
    'Checkbox': new FormControl(null)
  });
 
  ngOnInit(): void 
  {
    this.name = this.AuthenticationService.getUsername();
    this.Settings = this.UserService.getUserSettings();
    this.form.patchValue({"Minimum": this.Settings.sendAmount});
    this.form.patchValue({"Email": this.Settings.emailToSendTo});
    this.form.patchValue({"Checkbox": this.Settings.wantToRecieveNotification});
    
  }
 
  onSubmit() 
  {
    this.UserService.setUserSettingsInServer(this.form.get('Minimum')?.value, this.form.get('Email')?.value, this.form.get('Checkbox')?.value).subscribe(
          (response: string) =>
          {
            
          },
          (error) => console.log(error)
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
    this.AuthenticationService.logout();
    this.route.navigate(['/login']);
  }


  //I get this values from login
  // getSettingsFromServer()
  // {
  //   this.UserService.getUserSettingsFromServer().subscribe(
  //     (response: Settings ) =>
  //     {
  //       this.Settings = response;
  //       console.log(this.Settings);
  //       this.form.patchValue({"Minimum": this.Settings.Minimum});
  //       this.form.patchValue({"Email": this.Settings.Email});
  //       this.form.patchValue({"Checkbox": this.Settings.RecieveEmail});
  //       console.log(this.Settings.Minimum);
  //       console.log(this.Settings.Email);
  //       console.log(this.Settings.RecieveEmail);
  //     },
  //     (error) => console.log(error)
  //   );
  // }
}
