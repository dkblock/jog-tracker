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

const DeleteModal = ({
  deleteText,
  deleteButtonText = 'Delete',
  size = sizes.small,
  title,
  isDeleting,
  actions,
  onDelete,
  onClose,
}) => (
  <Dialog maxWidth={size} onClose={onClose} fullWidth open>
    <DialogTitle className="modal-common__header">
      <span className="modal-common__title">{title}</span>
      <IconButton type={IconButton.types.CLOSE} onClick={onClose} />
    </DialogTitle>
    <DialogContent className="modal-common__content" dividers>
      <Typography>{deleteText}</Typography>
    </DialogContent>
    <DialogActions>
      <div className="modal-common__actions">
        {!!actions && actions}
        <div className="modal-common__actions-buttons">
          <Button type={Button.types.text} onClick={onClose}>
            Cancel
          </Button>
          <Button color={Button.colors.danger} isLoading={isDeleting} onClick={onDelete}>
            {deleteButtonText}
          </Button>
        </div>
      </div>
    </DialogActions>
  </Dialog>
);

export default DeleteModal;
