import React from 'react';
import Dialog from '@mui/material/Dialog';

import sizes from '../constants/sizes';

const BorderlessModal = ({ children, onClose }) => (
  <Dialog maxWidth={sizes.large} onClose={onClose} open>
    <div onClick={onClose}>{children}</div>
  </Dialog>
);

export default BorderlessModal;
