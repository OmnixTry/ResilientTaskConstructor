import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function numericValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        const res = control.value + '';
        var regex = /^[0-9]\d*$/;
        return res.match(regex)?.length > 0 ? null : { notNumeric: true };
    };
}