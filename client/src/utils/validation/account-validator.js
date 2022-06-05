import { getValidationResult, isEmptyString } from './utils';

const accountValidator = () => {
  const validateName = (name, message) => {
    if (isEmptyString(name)) return `Enter ${message}`;
    return null;
  };

  const validatePassword = (password, validateLength = false) => {
    if (isEmptyString(password)) return 'Enter password';
    if (validateLength && password.length < 4) return 'The minimum length is 4 characters';
    return null;
  };

  const validateConfirmPassword = (confirmPassword, password) => {
    const error = validatePassword(confirmPassword);

    if (error) return error;
    if (password !== confirmPassword) return "Passwords don't match";
    return null;
  };

  return {
    validateOnRegister: (credentials) => {
      const { userName, firstName, lastName, password, confirmPassword } = credentials;
      const validationErrors = {
        userName: validateName(userName, 'username'),
        firstName: validateName(firstName, 'name'),
        lastName: validateName(lastName, 'last name'),
        password: validatePassword(password, true),
        confirmPassword: validateConfirmPassword(confirmPassword, password),
      };

      return getValidationResult(validationErrors);
    },

    validateOnLogin: ({ userName, password }) => {
      const validationErrors = {
        userName: validateName(userName, 'username'),
        password: validatePassword(password),
      };

      return getValidationResult(validationErrors);
    },
  };
};

export default accountValidator();
