import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function onlyLettersValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    if (!value) {
      return null; // No validar si está vacío
    }

    // Solo letras (mayúsculas o minúsculas)
    const letterRegex = /^[a-zA-Z]+$/;

    const isValid = letterRegex.test(value);

    return isValid ? null : { onlyLetters: true };
  };
}
