import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";


export function userNameValidator(maxLength: number): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    if (!value) {
      return null;
    }

    const isTooLong = value.length > maxLength;
    if (isTooLong) {
      return { 'userNameMaxlength': { max: maxLength } };
    }

    const prohibitedCharsRegex = /[@#\-()[\]+’¿!¡?$%^}{*.&]/;
    const hasProhibitedChars = prohibitedCharsRegex.test(value);
    if (hasProhibitedChars) {
      return { 'userNameProhibitedChars': true };
    }

    return null;
  };
}

export function useridentificationValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;



    return null;

  }
}

export function minMax(min: number, max: number, type: string): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    const isTooLong = value.length > max;
    if (isTooLong) {
      return { 'maxlengthtype': { max, type } };
    }
    const isTooShort = value.length < min;
    if (isTooShort) {
      return { 'minlengthtype': { min, type } };
    }
    return null;

  }
}
export function minMaxPhone(min: number, max: number, type: string): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    const isTooLong = value.length > max;
    if (isTooLong) {
      return { 'maxlengthtype': { max: max - 1, type } };
    }
    const isTooShort = value.length < min;
    if (isTooShort) {
      return { 'minlengthtype': { min: min - 1, type } };
    }
    return null;

  }
}

export function onlyNumericIdentification(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    
    if (!value) {
      return null;
    }
    const numericRegex = /^[0-9]*$/;
    const isNumeric = numericRegex.test(value);
    if (!isNumeric) {
      return { 'identificationNumeric': true };
    }
    return null;
  };
}
export function phoneNumeric(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null;
    }
    const numericRegex = /^[0-9]*$/;
    const isNumeric = numericRegex.test(value);
    if (!isNumeric) {
      return { 'phoneNumeric': true };
    }
    return null;
  };
}
