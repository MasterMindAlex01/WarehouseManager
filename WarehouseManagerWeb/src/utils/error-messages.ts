type ErrorMessage = string | ((error: any) => string);

export const ERROR_MESSAGES: { [key: string]: ErrorMessage } = {
  required: 'Campo obligatorio.',
  email: 'Formato inválido',
  minNumericLengthValidator: (error) =>
    `Debe tener al menos ${error.min} dígitos.`,
  min: (error) => `El valor mínimo permitido es ${error.min}.`,
  numericMaxLengthValidator: (maxLength) =>
    `El valor máximo permitido es de ${maxLength} dígitos.`,
  minlength: (error) =>
    `Debe tener al menos ${error.requiredLength} caracteres.`,
  maxlength: (error) =>
    `No puede exceder los ${error.requiredLength} caracteres.`,
  passwordStrength:
    'Mínimo 8 caracteres, con una mayúscula, una minúscula, un número y un caracter especial.',
  passwordCompleteStrength:
    'Mínimo 8 caracteres, con mayúscula, número y carácter especial.',
  companyMinLength: (error) =>
    `El nombre de la empresa debe tener al menos ${error.min} caracteres.`,
  companyMaxLength: (error) =>
    `El nombre de la empresa no puede exceder ${error.max} caracteres.`,
  useDescriptionMinLength: (error) =>
    `La descripción de uso debe tener al menos ${error.min} caracteres.`,
  useDescriptionMaxLength: (error) =>
    `La descripción de uso no puede exceder ${error.max} caracteres.`,
  roleMaxLength: (error) => 'Máx.30 caracteres',
  roleDescriptionMaxLength: (error) => 'Máx.200 caracteres',
  roleSpecialChars: (error) => 'El rol contiene caracteres inválidos',
  passwordMismatch: (error) => `Las contraseñas no coinciden`,
  userNameProhibitedChars: () => 'El nombre contiene caracteres inválidos',
  userNameMaxlength: (error) => `Max. ${error.max} caracteres`,
  minlengthtype: (error) => `Min. ${error.min} ${error.type}`,
  maxlengthtype: (error) => `Max. ${error.max} ${error.type}`,
  identificationNumeric: () =>
    'El número de identificación solo permite caracteres numéricos',
  phoneNumeric: () => 'El teléfono contiene caracteres inválidos',
  onlyLetters: () => 'Este campo solo permite letras',
  productError: () => 'El nombre del producto contiene caracteres inválidos',
  requiredField: () => 'Campo obligatorio',
};
