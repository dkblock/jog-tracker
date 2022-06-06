import React from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { deleteUser } from '../../redux/actions';
import { SELECTORS } from '../../redux';
import { Modal } from '../../components';

const DeleteUserModal = ({ user, onClose }) => {
  const dispatch = useDispatch();
  const { isSaving } = useSelector(SELECTORS.USERS.getFetching);

  return (
    <Modal.Delete
      title="Delete user"
      deleteText={`Do you really want to delete this user (${user.firstName} ${user.lastName})? All the results of his jogs will also be deleted`}
      isDeleting={isSaving}
      onClose={onClose}
      onDelete={() => dispatch(deleteUser({ userId: user.id }))}
    />
  );
};

export default DeleteUserModal;
