import React from 'react';
import { useDispatch } from 'react-redux';
import Dialog from '@mui/material/Dialog';

import { hideModal } from '../../actions';
import sizes from '../constants/sizes';

const BorderlessModal = ({ children }) => {
  const dispatch = useDispatch();
  const handleClose = () => dispatch(hideModal());

  return (
    <Dialog maxWidth={sizes.large} onClose={handleClose} open>
      <div onClick={handleClose}>{children}</div>
    </Dialog>
  );
};

export default BorderlessModal;
