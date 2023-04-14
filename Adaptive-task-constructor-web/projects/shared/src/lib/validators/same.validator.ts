import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function notSameValidator(control2: AbstractControl | null): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {

        //let first = control.get(contol1);
        //let second = control.get(control2);

        let notSame = (!!control?.value && !control2?.value)  || (!!control?.value && !!control2?.value && control?.value != control2?.value);// && first?.touched && second?.touched;
      return notSame ? { notSame: true } : null;
    };
}