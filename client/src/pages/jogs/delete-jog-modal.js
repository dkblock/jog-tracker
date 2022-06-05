import React from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { deleteJog } from '../../store/actions';
import { SELECTORS } from '../../store';
import { Modal } from '../../components';

const DeleteJogModal = ({ onClose }) => {
  const dispatch = useDispatch();
  const { props } = useSelector(SELECTORS.MODAL.props);
  const { isSaving } = useSelector(SELECTORS.JOGS.getFetching);
  const { jog } = props;

  return (
    <Modal.Delete
      title="Delete jog"
      deleteText="Do you really want to delete this jog?"
      isDeleting={isSaving}
      onClose={onClose}
      onDelete={() => dispatch(deleteJog({ jogId: jog.id }))}
    />
  );
};

export default DeleteJogModal;
