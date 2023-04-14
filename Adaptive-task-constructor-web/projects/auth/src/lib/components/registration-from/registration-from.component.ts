import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PasswordStrengthMeterService } from 'angular-password-strength-meter';
import { ValidationMessages } from 'projects/shared/src/lib/enum/validation-messages.enum';
import { pwdStrengthValidator, requireLowerLetters, requireNumbers, requireUpperLetters } from 'projects/shared/src/lib/validators/passwordStrength.validator';
import { notSameValidator } from 'projects/shared/src/lib/validators/same.validator';
import { Roles } from '../../entity/roles.enum';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'lib-registration-from',
  templateUrl: './registration-from.component.html',
  styleUrls: ['./registration-from.component.scss']
})
export class RegistrationFromComponent implements OnInit {
  validationMessages = ValidationMessages;
  roles = Roles;

  registrationForm: FormGroup;
  letterPattern = "^[a-zA-Z]*$";
  registrationErrors: string[];
  pwdErrorMessages = {
    weakPwd: 'Make a stronger password',
    requireLowerLetters: 'Password must contain lowercase letters',
    requireUpperLetters: 'Password must contain uppercase letters',
    requireNumbers: 'Password must contain numbers',
  }

  constructor(private formBuilder: FormBuilder, private registrationService: AuthenticationService, private strengthMeter: PasswordStrengthMeterService, private router: Router) { }

  ngOnInit(): void {
    this.registrationForm = this.formBuilder.group({
      "email": ["", [Validators.required]],
      "firstName": ["", [Validators.required, Validators.pattern(this.letterPattern)]],
      "lastName": ["", [Validators.required, Validators.pattern(this.letterPattern)]],
      "password": ["", [Validators.required, pwdStrengthValidator(3, this.strengthMeter), Validators.minLength(10), requireLowerLetters(), requireUpperLetters(), requireNumbers()]],
      "confirmPassword": ["", [Validators.required]],
      "passwordStrength": ["", [Validators.min(3)]],
      "role": ["", [Validators.required]]
    }, { updateOn: 'change' });

    let pwd = this.registrationForm.get('password');
    let confirmPwd = this.registrationForm.get('confirmPassword');
    confirmPwd?.setValidators(notSameValidator(pwd));
    this.registrationForm.valueChanges.subscribe(x => { console.log(this.registrationForm); });
  }

  onStrengthChange(strength: number) {
    this.registrationForm.get("passwordStrength")?.setValue(strength);
  }

  pwdBlur() {
    this.registrationForm.get("passwordStrength")?.markAsTouched();
  }

  onSubmit() {
    this.registrationService.registerUser(this.registrationForm.value)
      .subscribe(_ => {
        console.log("Successful registration");
        this.router.navigate(['/login']);
      },
        error => {
          console.log(error.error.errors);
          this.registrationErrors = error.error.errors;
        });
  }


}
