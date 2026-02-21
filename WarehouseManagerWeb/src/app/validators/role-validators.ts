import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function roleNameValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value as string;

    if (!value) {
      return null;
    }

    const specialChars = /[`!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/;

    if (value.length > 30) {
      return { roleMaxLength: true };
    }

    if (specialChars.test(value)) {
      return { roleSpecialChars: true };
    }

    return null;
  };
}
export function roleDescriptionValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value as string;

    if (!value) {
      return null;
    }

    if (value.length > 200) {
      return { roleDescriptionMaxLength: true };
    }

    return null;
  };
}