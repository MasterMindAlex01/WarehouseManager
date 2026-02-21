import { Pipe, PipeTransform } from '@angular/core';
import { ValidationErrors } from '@angular/forms';
import { ERROR_MESSAGES } from 'src/utils/error-messages';

@Pipe({
  name: 'formError',
  standalone: true,
})
export class FormErrorPipe implements PipeTransform {
  transform(errors: ValidationErrors | null | undefined): string {
    if (!errors) {
      return '';
    }

    if (errors['phoneNumeric']) {
      const messageOrFn = ERROR_MESSAGES['phoneNumeric'];
      return typeof messageOrFn === 'function'
        ? messageOrFn(errors['phoneNumeric'])
        : messageOrFn || 'El campo solo puede contener números.';
    }

    const messages: string[] = [];

    for (const [errorKey, errorValue] of Object.entries(errors)) {
      const messageOrFn = ERROR_MESSAGES[errorKey];
      if (!messageOrFn) continue;

      if (typeof messageOrFn === 'function') {
        messages.push(messageOrFn(errorValue));
      } else {
        messages.push(messageOrFn);
      }
    }

    return messages.join(' • ');
  }
}
