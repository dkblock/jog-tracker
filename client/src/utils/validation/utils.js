export const isEmptyString = (str) => !str || /^\s*$/.test(str);

export const getValidationResult = (errors) => ({
  isValid: isValid(errors),
  errors,
});

const isValid = (errors) => Object.keys(errors).filter((key) => errors[key] !== null).length === 0;
