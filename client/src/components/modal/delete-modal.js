import React from 'react';
import { useDispatch } from 'react-redux';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import Typography from '@mui/material/Typography';

import { hideModal } from '../../actions';
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
}) => {
  const dispatch = useDispatch();
  const handleClose = () => dispatch(hideModal());

  return (
    <Dialog maxWidth={size} onClose={handleClose} fullWidth open>
      <DialogTitle className="modal-common__header">
        <span className="modal-common__title">{title}</span>
        <IconButton type={IconButton.types.close} onClick={handleClose} />
      </DialogTitle>
      <DialogContent className="modal-common__content" dividers>
        <Typography>{deleteText}</Typography>
      </DialogContent>
      <DialogActions>
        <div className="modal-common__actions">
          {!!actions && actions}
          <div className="modal-common__actions-buttons">
            <Button type={Button.types.text} onClick={handleClose}>
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
};

export default DeleteModal;
