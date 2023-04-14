import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function atLeastOneValidator(predicate: (val: any) => boolean): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        const res = (control.value as any[]).some(predicate);
        return res ? null : { notOne: true };
    };
}