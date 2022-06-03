export const isEmptyString = (str) => !str || /^\s*$/.test(str);

export const getValidationResult = (validationErrors) => ({
  isValid: isValid(validationErrors),
  validationErrors,
});

const isValid = (validationErrors) =>
  Object.keys(validationErrors).filter((key) => validationErrors[key] !== null).length === 0;
