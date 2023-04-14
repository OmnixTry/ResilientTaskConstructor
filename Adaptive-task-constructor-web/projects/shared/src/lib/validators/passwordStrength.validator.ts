import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { PasswordStrengthMeterService } from "angular-password-strength-meter";

export function pwdStrengthValidator(min: number, meter: PasswordStrengthMeterService): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      return meter.score(control.value) < min ? { 'weakPwd': true } : null;
    };
}

export function requireLowerLetters(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const regEx = new RegExp('^.*[a-z]+.*$');
    return !(control.value as string).match(regEx) ? {requireLowerLetters: true} : null;
  };
}

export function requireUpperLetters(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const regEx = new RegExp('^.*[A-Z]+.*$');
    return !(control.value as string).match(regEx) ? {requireUpperLetters: true} : null;
  };
}

export function requireNumbers(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const regEx = new RegExp('^.*[0-9]+.*$');
    return !(control.value as string).match(regEx) ? {requireNumbers: true} : null;
  };
}