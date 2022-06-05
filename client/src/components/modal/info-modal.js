import React from 'react';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import Typography from '@mui/material/Typography';

import sizes from '../constants/sizes';
import { Button } from '../button';
import { IconButton } from '../icon';
import './modal.scss';

const InfoModal = ({ infoText, okButtonText = 'OK', size = sizes.small, title, onSubmit, onClose }) => (
  <Dialog maxWidth={size} onClose={onClose} fullWidth open>
    <DialogTitle className="modal-common__header">
      <span className="modal-common__title">{title}</span>
      <IconButton type={IconButton.types.CLOSE} onClick={onClose} />
    </DialogTitle>
    <DialogContent className="modal-common__content" dividers>
      <Typography>{infoText}</Typography>
    </DialogContent>
    <DialogActions>
      <Button type={Button.types.text} onClick={onClose}>
        Cancel
      </Button>
      <Button onClick={onSubmit}>{okButtonText}</Button>
    </DialogActions>
  </Dialog>
);

export default InfoModal;
