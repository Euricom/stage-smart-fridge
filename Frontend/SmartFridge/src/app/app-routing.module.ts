import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SettingsComponent } from './settings/settings.component';
import { AuthGuardService } from './config/auth-guard.service';
import { MenuComponent } from './menu/menu.component';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'menu', component: MenuComponent, children: 
    [
      { path: 'home', component: HomeComponent, canActivate: [AuthGuardService] },
      { path: 'settings', component: SettingsComponent, canActivate: [AuthGuardService] },
    ], canActivate: [AuthGuardService]
  },
  { path: '**', component: HomeComponent, canActivate: [AuthGuardService]}
  ];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
