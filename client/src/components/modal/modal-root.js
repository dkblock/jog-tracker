import React from 'react';
import { useSelector } from 'react-redux';

import modalTypes from './modal-types';

const modals = {
  [modalTypes.CREATE_JOG]: null,
};

const ModalRoot = () => {
  const { modalType, modalProps } = useSelector((state) => state.MODAL);

  if (!modalType) return null;

  const SpecificModal = modals[modalType];
  return <SpecificModal {...modalProps} />;
};

export default ModalRoot;
