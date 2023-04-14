import { VERSION } from "@angular/core";
import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function notIncludeValidator(arr: any[]): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      let any = arr.includes(control.value);
      return any ? { includes: true } : null;
    };
}