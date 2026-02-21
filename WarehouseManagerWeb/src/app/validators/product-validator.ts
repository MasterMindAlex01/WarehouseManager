import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function productNameValidator(
  control: AbstractControl
): ValidationErrors | null {
  if (typeof control.value !== 'string') return null;

  let value = control.value.trim().replace(/\s{2,}/g, ' ');

  if (value.length < 3) {
    return { minlengthtype: { min: 3, type: 'caracteres' } };
  }
  if (value.length > 30) {
    return { maxlengthtype: { max: 30, type: 'caracteres' } };
  }

  const invalidChars = /[@#\-\(\)\+’¿!¡\?\$%\^}{\*\.,\/\\_:;]/;
  if (invalidChars.test(value)) {
    return { productError: true };
  }

  return null;
}

export function minSelectedValidator(min: number): ValidatorFn {
  return (control: AbstractControl) => {
    const value = control.value;
    return Array.isArray(value) && value.length >= min
      ? null
      : { minSelected: true };
  };
}
