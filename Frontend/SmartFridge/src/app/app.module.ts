import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SettingsComponent } from './settings/settings.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule} from '@angular/material/card';
import { MatToolbarModule} from '@angular/material/toolbar';
import { MatInputModule} from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule} from '@angular/material/icon';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatRadioModule} from '@angular/material/radio';
import {MatMenuModule} from '@angular/material/menu';
import {MatExpansionModule} from '@angular/material/expansion';
import { Interceptor } from './config/Interceptor';
import {MatTableModule} from '@angular/material/table';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { AuthenticationService } from './services/authentication/authentication.service';
 import { AuthGuardService } from './config/auth-guard.service';
 import { TableService} from './services/table/table.service';
 import { UserService } from './services/users/user.service';
 import { ConfirmEqualValidator } from './models/confirmEqualValidator';
 import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MenuComponent } from './menu/menu.component';
import {DatePipe} from '@angular/common';





@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    SettingsComponent,
    ConfirmEqualValidator,
    MenuComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCardModule,
    MatToolbarModule,
    MatInputModule,
    MatIconModule,
    ReactiveFormsModule,
    MatSidenavModule,
    MatRadioModule,
    MatMenuModule,
    MatExpansionModule,
    MatTableModule,
    MatCheckboxModule,
    MatSnackBarModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: Interceptor, multi: true 
    },
    AuthenticationService, 
    AuthGuardService, 
    TableService,
    UserService,
    DatePipe
    ],
  
  bootstrap: [AppComponent]
})
export class AppModule { }
