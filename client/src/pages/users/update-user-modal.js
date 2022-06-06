import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { updateUser } from '../../redux/actions';
import { useCurrentUser } from '../../hooks';
import { SELECTORS } from '../../redux';
import roles from '../../constants/roles';
import { Dropdown, Modal, TextField } from '../../components';

const roleItems = [
  { value: roles.ADMINISTRATOR, label: 'Administrator' },
  { value: roles.USER, label: 'User' },
];

const UpdateUserModal = ({ user, onClose }) => {
  const dispatch = useDispatch();
  const { isSaving } = useSelector(SELECTORS.USERS.getFetching);
  const serverErrors = useSelector(SELECTORS.USERS.getErrors);
  const { currentUser } = useCurrentUser();

  const [firstName, setFirstName] = useState(user.firstName);
  const [lastName, setLastName] = useState(user.lastName);
  const [role, setRole] = useState(user.role);
  const [errors, setErrors] = useState({});

  useEffect(() => {
    setErrors({ ...errors, ...serverErrors });
  }, [serverErrors]);

  const handleFirstNameChange = (value) => setFirstName(value);
  const handleLastNameChange = (value) => setLastName(value);
  const handleRoleChange = (value) => setRole(value);

  const handleFirstNameFocus = () => setErrors((prev) => ({ ...prev, firstName: null }));
  const handleLastNameFocus = () => setErrors((prev) => ({ ...prev, lastName: null }));

  const handleSubmit = () => {
    const updatedUser = { ...user, firstName, lastName, role };
    dispatch(updateUser({ user: updatedUser }));
  };

  return (
    <Modal.Create
      title="Edit user"
      createButtonText="Edit"
      isCreating={isSaving}
      onClose={onClose}
      onCreate={handleSubmit}
    >
      <form className="input-form">
        <TextField
          className="w-100"
          label="First name"
          value={firstName}
          error={Boolean(errors.firstName)}
          helperText={errors.firstName}
          onChange={handleFirstNameChange}
          onFocus={handleFirstNameFocus}
        />
        <TextField
          className="w-100"
          label="Last name"
          value={lastName}
          error={Boolean(errors.lastName)}
          helperText={errors.lastName}
          onChange={handleLastNameChange}
          onFocus={handleLastNameFocus}
        />
        <Dropdown
          className="w-100"
          label="Role"
          items={roleItems}
          value={role}
          disabled={currentUser.id === user.id}
          onChange={handleRoleChange}
        />
      </form>
    </Modal.Create>
  );
};

export default UpdateUserModal;
