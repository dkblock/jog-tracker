import { createSlice } from '@reduxjs/toolkit';
import { showModal, hideModal } from '../../actions';

const initialState = {
  modalType: null,
  modalProps: null,
};

const modalReducer = createSlice({
  name: 'MODAL',
  initialState: initialState,
  extraReducers: {
    [showModal]: (state, { payload: { modalType, modalProps } }) => {
      state.modalType = modalType;
      state.modalProps = modalProps;
    },
    [hideModal]: (state) => {
      state.modalType = null;
      state.modalProps = null;
    },
  },
}).reducer;

export { modalReducer };
