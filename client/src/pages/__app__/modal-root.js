import React from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { hideModal } from '../../store/actions';
import { SELECTORS } from '../../store';
import modalTypes from '../../constants/modal-types';
import CreateJogModal from '../jogs/create-jog-modal';
import DeleteJogModal from '../jogs/delete-jog-modal';
import UpdateJogModal from '../jogs/update-jog-modal';

const modals = {
  [modalTypes.CREATE_JOG]: CreateJogModal,
  [modalTypes.DELETE_JOG]: DeleteJogModal,
  [modalTypes.UPDATE_JOG]: UpdateJogModal,
};

const ModalRoot = () => {
  const dispatch = useDispatch();
  const { type, props } = useSelector(SELECTORS.MODAL.props);

  if (!type) {
    return null;
  }

  const SpecificModal = modals[type];
  return <SpecificModal onClose={() => dispatch(hideModal())} {...props} />;
};

export default ModalRoot;
