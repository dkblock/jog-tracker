import { createAction } from '@reduxjs/toolkit';
import modalTypes from '../../constants/modal-types';

const getPayload = (modalType, modalProps) => ({ payload: { modalType, modalProps } });

export const showModal = createAction('showModal');
export const hideModal = createAction('hideModal');

export const showCreateJogModal = createAction('showModal', () => getPayload(modalTypes.CREATE_JOG));
export const showDeleteJogModal = createAction('showModal', ({ jog }) => getPayload(modalTypes.DELETE_JOG, { jog }));
export const showUpdateJogModal = createAction('showModal', ({ jog }) => getPayload(modalTypes.UPDATE_JOG, { jog }));

export const showDeleteUserModal = createAction('showModal', ({ user }) =>
  getPayload(modalTypes.DELETE_USER, { user }),
);
export const showUpdateUserModal = createAction('showModal', ({ user }) =>
  getPayload(modalTypes.UPDATE_USER, { user }),
);
