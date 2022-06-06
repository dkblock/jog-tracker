import React from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { hideModal } from '../../redux/actions';
import { SELECTORS } from '../../redux';
import modalTypes from '../../constants/modal-types';

import CreateJogModal from '../jogs/create-jog-modal';
import DeleteJogModal from '../jogs/delete-jog-modal';
import UpdateJogModal from '../jogs/update-jog-modal';

import GenerateReportModal from '../jogs/generate-report-modal';

import DeleteUserModal from '../users/delete-user-modal';
import UpdateUserModal from '../users/update-user-modal';

const modals = {
  [modalTypes.CREATE_JOG]: CreateJogModal,
  [modalTypes.DELETE_JOG]: DeleteJogModal,
  [modalTypes.UPDATE_JOG]: UpdateJogModal,

  [modalTypes.GENERATE_REPORT]: GenerateReportModal,

  [modalTypes.DELETE_USER]: DeleteUserModal,
  [modalTypes.UPDATE_USER]: UpdateUserModal,
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
