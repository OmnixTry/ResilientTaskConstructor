import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { IPasswordStrengthMeterService, PasswordStrengthMeterModule, PasswordStrengthMeterService } from 'angular-password-strength-meter';
import { HttpRequestService, SharedModule } from 'projects/shared/src/public-api';
import { AuthComponent } from './auth.component';
import { LogInFormComponent } from './components/log-in-form/log-in-form.component';
import { RegistrationFromComponent } from './components/registration-from/registration-from.component';
import { MatSelectModule } from '@angular/material/select';
import { ForbiddenPageComponent } from './components/forbidden-page/forbidden-page.component';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from "@angular/material/form-field";
import { RoleDirective } from './directives/role.directive';

export const routes: Routes = [
  {
    path: 'login',
    component: LogInFormComponent,
  },
  {
    path: 'register',
    component: RegistrationFromComponent,
  }  
];

@NgModule({
  declarations: [
    AuthComponent,
    RegistrationFromComponent,
    LogInFormComponent,
    RoleDirective
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    SharedModule,
    PasswordStrengthMeterModule.forRoot(),
    MatSelectModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    AuthComponent,
    RouterModule,
    RoleDirective
  ],
  providers: [HttpRequestService, PasswordStrengthMeterService, RoleDirective]
})
export class AuthModule { }
